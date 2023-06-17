/*
import React, { useState } from 'react';

const ChatPage = () => {
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');

    const sendMessage = () => {
        if (input.trim() !== '') {
            const newMessage = {
                content: input,
                sender: 'user',
            };
            setMessages([...messages, newMessage]);
            setInput('');
        }
    };

    return (
        <div>
            <h1>Chat Page</h1>
    <div className="messages">
        {messages.map((message, index) => (
                <div key={index} className={`message ${message.sender}`}>
        {message.content}
        </div>
))}
    </div>
    <div className="input-container">
    <input
        type="text"
    value={input}
    onChange={(e) => setInput(e.target.value)}
    onKeyPress={(e) => {
        if (e.key === 'Enter') {
            sendMessage();
        }
    }}
    placeholder="Type your message..."
    />
    <button onClick={sendMessage}>Send</button>
        </div>
        </div>
);
};

export default ChatPage;
*/


/*import {Container, Header, Label} from "semantic-ui-react";
import List from "semantic-ui-react/dist/commonjs/elements/List";
import Segment from "semantic-ui-react/dist/commonjs/elements/Segment";
import Form from "semantic-ui-react/dist/commonjs/collections/Form";
import Button from "semantic-ui-react/dist/commonjs/elements/Button";

const ChatComponent = () => {
    const messages = [
        { id: 1, text: 'Hello' },
        { id: 2, text: 'How are you?' },
        // ...
    ];

    return (
        <Container>
            <Segment>
                <List relaxed divided>
                    {messages.map(message => (
                        <List.Item key={message.id}>{message.text}</List.Item>
                    ))}
                </List>
            </Segment>
            <Segment>
                <Form>
                    <Header as='h1' textAlign='center' className="main-header">Upstorage Chat</Header>
                    <label>Name</label>
                    <Form.Input placeholder="Type your message..." />
                    <Button primary onClick={sendMessage}>JoinChat</Button>
                </Form>
            </Segment>
        </Container>
    );
};

export default ChatComponent;*/

/*import React, { useState } from 'react';
import { Input, Button, Message } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';

const ChatPage = () => {
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');

    const sendMessage = () => {
        if (input.trim() !== '') {
            const newMessage = {
                content: input,
                sender: 'user',
            };
            setMessages([...messages, newMessage]);
            setInput('');
        }
    };

    return (
        <div>
            <h1>Chat Page</h1>
            <div className="messages">
                {messages.map((message, index) => (
                    <Message
                        key={index}
                        content={message.content}
                        className={message.sender}
                    />
                ))}
            </div>
            <div className="input-container">
                <Input
                    fluid
                    value={input}
                    onChange={(e) => setInput(e.target.value)}
                    onKeyPress={(e) => {
                        if (e.key === 'Enter') {
                            sendMessage();
                        }
                    }}
                    placeholder="Type your message..."
                />
                <Button primary onClick={sendMessage}>
                    Send
                </Button>
            </div>
        </div>
    );
};

export default ChatPage;*/


import React, {useEffect, useState} from 'react';
import {Input, Button, Message, Segment, Header, Form, Checkbox} from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import api from "../utils/AxiosInstance";
import {useNavigate} from "react-router-dom";
import ChatPage from "./ChatPage";

const ChatLoginPage = () => {
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');
    const [user, setUser] = useState('');

    const[pageSecond, setPageSecond] = useState(false);

    const navigate = useNavigate();

    const handleSubmit = async () => {
       setPageSecond(true);
       navigate(`/message/${user}`);
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUser(e.target.value);
    }


    return (
          <Segment padded='very'>
                        <Header as='h1' textAlign='center' className="main-header">Upstorage Chat</Header>
                        <Form onSubmit={handleSubmit}>
                            <Form.Field>
                                <label>Name</label>
                                <Input placeholder='Username' name="userName" onChange={handleChange} />
                            </Form.Field>
                            <Button type='join chat'>Submit</Button>
                        </Form>
                    </Segment>

                )
};

export default ChatLoginPage;
