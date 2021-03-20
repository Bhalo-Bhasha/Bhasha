﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bhasha.Common;
using Bhasha.Common.Services;
using Bhasha.Common.Tests.Support;
using Bhasha.Web.Controllers;
using Bhasha.Web.Services;
using FakeItEasy;
using NUnit.Framework;

namespace Bhasha.Web.Tests.Controllers
{
    [TestFixture]
    public class PageControllerTests
    {
        private IDatabase _database;
        private IAuthorizedProfileLookup _profiles;
        private IEvaluateSubmit _evaluator;
        private IUpdateStatsForTip _stateUpdater;
        private PageController _controller;

        [SetUp]
        public void Before()
        {
            _database = A.Fake<IDatabase>();
            _profiles = A.Fake<IAuthorizedProfileLookup>();
            _evaluator = A.Fake<IEvaluateSubmit>();
            _stateUpdater = A.Fake<IUpdateStatsForTip>();
            _controller = new PageController(_database, _profiles, _evaluator, _stateUpdater);
        }

        [Test]
        public async Task Submit([Values]Result result)
        {
            var profile = ProfileBuilder.Default.Build();
            var submit = new Submit(Guid.NewGuid(), 1, "something");

            A.CallTo(() => _profiles.Get(profile.Id, _controller.UserId))
                .Returns(Task.FromResult(profile));

            var evaluation = new Evaluation(result, submit);

            A.CallTo(() => _evaluator.Evaluate(profile, A<Submit>.That.Matches(x => x.Equals(submit))))
                .Returns(Task.FromResult(evaluation));

            var eval = await _controller.Submit(
                profile.Id, submit.ChapterId, submit.PageIndex, submit.Solution);

            Assert.That(eval, Is.EqualTo(evaluation));
        }

        [Test]
        public async Task Tip()
        {
            var chapterId = Guid.NewGuid();
            var pageIndex = 1;
            var profile = ProfileBuilder.Default.Build();

            A.CallTo(() => _profiles.Get(profile.Id, _controller.UserId))
                .Returns(Task.FromResult(profile));

            var tips = new[] { TipBuilder.Default.Build() };

            A.CallTo(() => _database.QueryTips(chapterId, pageIndex))
                .Returns(Task.FromResult<IEnumerable<Tip>>(tips));

            var result = await _controller.Tip(profile.Id, chapterId, pageIndex);

            var expectedTip = tips.Single();

            Assert.That(result, Is.EqualTo(expectedTip));

            A.CallTo(() => _stateUpdater.UpdateStats(expectedTip, profile))
                .MustHaveHappenedOnceExactly();
        }
    }
}