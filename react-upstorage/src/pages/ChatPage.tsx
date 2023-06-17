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
import {ApiResponse} from "../types/GenericTypes";
import SignalRService from "./SignalRService";
import GridRow from "semantic-ui-react/dist/commonjs/collections/Grid/GridRow";
import Grid from "semantic-ui-react/dist/commonjs/collections/Grid";
import List from "semantic-ui-react/dist/commonjs/elements/List";
import { useParams } from 'react-router-dom';



const ChatPage = () => {
    const { userName } = useParams();
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');
    const [users, setUsers] = useState([]);
    const [signalRService, setSignalRService] = useState<SignalRService | undefined>(undefined);

const usersyyyy = ["user", "user3"];
    const chatHistory = [
        { sender: 'User 1', message: 'Hello' },
        { sender: 'User 2', message: 'Hi there' },
        { sender: 'User 1', message: 'How are you?' },
        { sender: 'User 2', message: 'I\'m good, thanks!' },
    ];



    useEffect(() => {

        const temp = async () =>{
            const signalRService = new SignalRService();
           await  signalRService.startConnection();

            if (userName) await signalRService.invokeAddUser(userName);


           await signalRService.invokeGetAllUsers()
                .then((users) => {
                    setUsers(users);
                })
                .catch((error) => {
                    console.log('Error retrieving users:', error);
                });
            setSignalRService(signalRService);
        }

        temp();

    }, []);


    const sendMessage = async () =>{


      /*  const users = await setSignalRService.invoke<string[]>('GetAllUsers');*/
    }


    return (
       /* <div>
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

        </div>*/

        <Grid columns={2} divided>
            <Grid.Column>

                  <Segment>
                      <h3>User List</h3>
                      <List divided relaxed>
                          {users.map((user, index) => (
                              <List.Item key={index}>
                                  <List.Icon name="user" size="large" verticalAlign="middle" />
                                  <List.Content>{user}</List.Content>
                              </List.Item>
                          ))}
                      </List>
                  </Segment>

            </Grid.Column>
            <Grid.Column>
                <Segment>
                    <h3>Chat History</h3>
                    <List relaxed>
                        {chatHistory.map((chat, index) => (
                            <List.Item key={index}>
                                <List.Icon name="comment" size="large" verticalAlign="middle" />
                                <List.Content>
                                    <List.Header>{chat.sender}</List.Header>
                                    <List.Description>{chat.message}</List.Description>
                                </List.Content>
                            </List.Item>))}
                    </List>
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
                </Segment>
            </Grid.Column>
        </Grid>

    );
};

export default ChatPage;
