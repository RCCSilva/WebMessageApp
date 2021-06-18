import * as S from "./styles";

type Props = {
  user: string;
  origin: string;
  message: string;
};

const MessageCard = ({ user, origin, message }: Props) => {
  return (
    <S.Card isCurrentUser={user === origin}>
      <S.Container isCurrentUser={user === origin}>
        <S.Origin>{origin}:</S.Origin>
        <S.Message>{message}</S.Message>
      </S.Container>
    </S.Card>
  );
};

export default MessageCard;
