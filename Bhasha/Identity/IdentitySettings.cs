﻿using Microsoft.Extensions.Configuration;

namespace Bhasha.Shared.Identity;

public class DefaultUser
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Role { get; set; } = string.Empty;
}

public class IdentitySettings
{
	public DefaultUser[] DefaultUsers { get; }

	public IdentitySettings(DefaultUser[] defaultUsers)
	{
		DefaultUsers = defaultUsers;
	}

	public static IdentitySettings From(IConfiguration configuration)
	{
		var section = configuration.GetSection("Identity:DefaultUsers");
		var defaultUsers = section.Get<DefaultUser[]>() ?? Array.Empty<DefaultUser>();

		return new IdentitySettings(defaultUsers);
	}
}