import type { Message } from "../../App";
import MessageCard from "../MessageCard";
import * as S from "./styles";

type Props = {
  messages: Array<Message>
  user: string
};
const MessageList = ({ messages, user }: Props) => {
  return (
    <S.MessageList>
      {messages.map((m) => (
        <MessageCard user={user} origin={m.fromUser} message={m.message} />
      ))}
    </S.MessageList>
  );
};

export default MessageList;
