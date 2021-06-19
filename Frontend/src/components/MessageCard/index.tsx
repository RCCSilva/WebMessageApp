import { MessageState } from "../../App";
import * as S from "./styles";
import MessageInCard from './components/MessageInCard'
import MessageOutCard from './components/MessageOutCard'

type Props = {
  message: MessageState
}

const MessageCard = ({ message }: Props) => {
  return (
    <S.Container isIn={message.isIn} data-testid='message-row'>
      {message.isIn ? <MessageInCard message={message.message} /> : <MessageOutCard message={message.message} />}
    </S.Container>
  );
};

export default MessageCard;
