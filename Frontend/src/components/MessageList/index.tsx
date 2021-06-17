import type { Message } from "../../App";

type MessageListInput = {
  messages: Array<Message>;
};

const MessageList = ({ messages }: MessageListInput) => {
  return (
    <div>
      {messages.map((message) => (
        <div>
          <label>
            Origem:
            <p>{message.fromUser}</p>
          </label>
          <label>
            Mensagem:
            <p>{message.message}</p>
          </label>
        </div>
      ))}
    </div>
  );
};

export default MessageList;
