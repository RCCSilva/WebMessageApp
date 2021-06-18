import "./App.css";
import { useEffect, useState } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from "@microsoft/signalr";
import MessageForm from "./components/MessageForm";
import LoginForm from "./components/LoginForm";
import MessageList from "./components/MessageList";

type Message = {
  fromUser: string;
  toUser: string;
  message: string;
};

const App = () => {
  const [connection, setConnection] = useState<HubConnection>();
  const [user, setUser] = useState<string>('');
  const [messsages, setMessages] = useState<Array<Message>>([]);

  useEffect(() => {
    if (connection === undefined) {
      const conn = new HubConnectionBuilder()
        .withUrl("https://localhost:5001/chathub")
        .withAutomaticReconnect()
        .build();
      setConnection(conn);
    }
  }, [connection]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log("Connected!");

          connection.on("ReceiveMessage", (message: Message) => {
            setMessages((m) => [...m, message]);
          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const sendToWebsocket = async (topic: string, data: any) => {
    if (connection?.state === HubConnectionState.Connected) {
      try {
        await connection?.send(topic, data);
      } catch (e) {
        console.log(e);
      }
    } else {
      alert("No connection started");
    }
  };

  const sendMessage = async (toUser: string, message: string) => {
    const chatMessage = {
      fromUser: user,
      toUser: toUser,
      message: message,
    };
    setMessages((m) => [...m, chatMessage]);
    sendToWebsocket("SendMessage", chatMessage);
  };

  const sendLogin = async (username: string) => {
    sendToWebsocket("Connect", username);
    setUser(username);
  };

  return (
    <div className="App">
      {user ? (
        <>
          <h1>Olá, {user}!</h1>
          <MessageForm sendMessage={sendMessage} />
          <MessageList user={user} messages={messsages} />
        </>
      ) : (
        <LoginForm sendLogin={sendLogin} />
      )}
    </div>
  );
};

export type { Message };
export default App;
