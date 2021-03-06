import { Breadcrumbs, Card, CardContent, Link } from '@material-ui/core';
import React from 'react';
import { DISPLAY_CHAPTER, DISPLAY_CHAPTER_SELECTION, DISPLAY_PROFILE_SELECTION } from '../consts';

function NavigationBar(props) {
    const screen = props.screen;
    
    const language = 
        props.profile !== undefined ? 
        props.profile.target.name.toUpperCase() : null;

    const level =
        props.profile !== undefined ?
        props.profile.level : null;

    const chapter = 
        props.chapter !== undefined ? 
        props.chapter.name.native.toUpperCase() : null;

    return (
        <Card>
            <CardContent>
                <Breadcrumbs aria-label="breadcrumb">
                    { screen === DISPLAY_PROFILE_SELECTION &&
                        <Link
                          color="inherit"
                          component="button"
                          variant="body2">SELECT PROFILE</Link>
                    }
                    { screen !== DISPLAY_PROFILE_SELECTION &&
                        <Link 
                          onClick={props.onSelectProfile} 
                          component="button"
                          variant="body2">{language}</Link>
                    }
                    { screen === DISPLAY_CHAPTER_SELECTION &&
                        <Link
                          color="inherit"
                          component="button"
                          variant="body2">LEVEL {level}</Link>
                    }
                    { screen === DISPLAY_CHAPTER &&
                        <Link
                          onClick={props.onSelectChapter}
                          component="button"
                          variant="body2">{chapter}</Link>
                    }
                </Breadcrumbs>
            </CardContent>
        </Card>
    );
}

export default NavigationBar;