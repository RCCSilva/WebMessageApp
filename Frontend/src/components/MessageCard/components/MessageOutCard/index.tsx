import * as S from "../../styles";

type Props = {
  message: string;
};

const MessageInCard = ({ message }: Props) => {
  return (
    <S.MessageOutCard data-testid="message-in-card">
      <S.Message>{message}</S.Message>
    </S.MessageOutCard>
  );
};

export default MessageInCard;