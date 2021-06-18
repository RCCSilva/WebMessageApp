import { useState } from "react";
import * as S from "./styles";

type MessageHandler = {
  sendMessage: (message: string, toUser: string) => any;
};

const MessageForm = ({ sendMessage }: MessageHandler) => {
  const [message, setMessage] = useState("");
  const [toUser, setToUser] = useState("");

  const handleSubmit = (event: React.SyntheticEvent) => {
    event.preventDefault();

    sendMessage(toUser, message);
    setMessage("");
  };

  return (
    <S.Form onSubmit={handleSubmit}>
      <S.Label>
        Destinatário:
        <S.Input
          type="text"
          value={toUser}
          onChange={(e) => setToUser(e.target.value)}
        />
      </S.Label>
      <S.Label>
        Mensagem:
        <S.Input
          type="text"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
        />
      </S.Label>
      <S.SendMessageButton type="submit" value="Enviar Mensagem" />
    </S.Form>
  );
};

export default MessageForm;
