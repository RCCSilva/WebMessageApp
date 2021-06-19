import type { MessageState } from "../../App";
import MessageCard from "../MessageCard";
import * as S from "./styles";

type Props = {
  messages: Array<MessageState>
};

const MessageList = ({ messages }: Props) => {
  return (
    <S.MessageList>
      {messages.map((m) => (
        <MessageCard message={m} />
      ))}
    </S.MessageList>
  );
};

export default MessageList;
