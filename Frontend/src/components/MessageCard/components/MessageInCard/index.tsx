import * as S from "../../styles";

type Props = {
  message: string;
};

const MessageInCard = ({ message }: Props) => {
  return (
    <S.MessageInCard data-testid="message-in-card">
      <S.Message>{message}</S.Message>
    </S.MessageInCard>
  );
};

export default MessageInCard;
