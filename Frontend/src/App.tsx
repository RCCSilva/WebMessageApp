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

type MessageState = {
  fromUser: string;
  toUser: string;
  message: string;
  isIn: boolean;
};

const App = () => {
  const [connection, setConnection] = useState<HubConnection>();
  const [user, setUser] = useState<string>("");
  const [messsages, setMessages] = useState<Array<MessageState>>([]);
  const [isConnected, setIsConnected] = useState<boolean>(false);

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
      connection.onreconnecting(() => setIsConnected(false));
      connection.onreconnected(() => setIsConnected(true));
      connection.onclose(() => console.log("Closed"));

      connection.start().then(() => {
        setIsConnected(true)
        connection.on("ReceiveMessage", (message: Message) => {
          const newMessage = { ...message, isIn: true };
          setMessages((m) => [...m, newMessage]);
        });
      });
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
      throw Error("Connection not set");
    }
  };

  const sendMessage = async (toUser: string, message: string) => {
    const chatMessage = {
      fromUser: user,
      toUser: toUser,
      message: message,
    };
    const newMessage = { ...chatMessage, isIn: false };
    setMessages((m) => [...m, newMessage]);
    sendToWebsocket("SendMessage", chatMessage);
  };

  const sendLogin = async (username: string) => {
    sendToWebsocket("Connect", username)
      .then(() => {
        console.log("Oi!");
        setUser(username);
      })
      .catch((e) => console.error(e));
  };

  return (
    <>
      {isConnected ? <p>Conectado</p> : <p>Reconectando</p>}
      {user ? (
        <>
          <h1>Olá, {user}!</h1>
          <MessageForm sendMessage={sendMessage} />
          <MessageList messages={messsages} />
        </>
      ) : (
        <LoginForm sendLogin={sendLogin} />
      )}
    </>
  );
};

export type { Message, MessageState };
export default App;
