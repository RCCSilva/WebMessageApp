import { useState } from "react";
import * as S from "./styles";

type LoginHandler = {
  sendLogin: (username: string) => any;
};

const LoginForm = ({ sendLogin }: LoginHandler) => {
  const [username, setUsername] = useState("");

  const handleSubmit = (event: React.SyntheticEvent) => {
    event.preventDefault();

    sendLogin(username);
  };

  return (
    <S.Form onSubmit={handleSubmit}>
      <S.Label>
        Como você deseja ser chamado?
        <S.Input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </S.Label>
      <S.LoginInput type="submit" value="Login" />
    </S.Form>
  );
};

export default LoginForm;
