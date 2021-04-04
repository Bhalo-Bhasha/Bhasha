import React, { useEffect } from 'react';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { api } from '../utils';

function ChapterSelection(props) {
    const [chapters, setChapters] = React.useState([]);
    const [loading, setLoading] = React.useState(true);

    const onSelect = (chapter) => () => {
        props.onSelect(chapter);
    }

    useEffect(() => {
        api
          .get(`api/chapter/list?profileId=${props.profile.id}`)
          .then(res => {
              setChapters(res.data);
              setLoading(false);
            });
    }, [setChapters, props]);

    if (loading)
    {
        return <div>... loading ...</div>
    }

    const createItem = chapter => {
        if (chapter.completed || props.completedIds.includes(chapter.id))
        {
            return (
                <ListItem disabled>
                    <ListItemText>{chapter.name.toUpperCase()}</ListItemText>
                </ListItem>);    
        }
        else
        {
            return (
                <ListItem button onClick={onSelect(chapter)}>
                    <ListItemText>{chapter.name.toUpperCase()}</ListItemText>
                </ListItem>);
        }
    };

    return (
        <div>
            <List component="nav">
                { chapters.map(createItem) }
            </List>
        </div>
    );
}

export default ChapterSelection;