import styled from "styled-components";

interface Props {
  isCurrentUser: boolean;
}

export const Card = styled.div<Props>`
  width: 36rem;

  display: flex;
  flex-direction: ${(props) => (props.isCurrentUser ? "row-reverse" : "row")};

  padding: 1rem 0 1rem 1.5rem;
`;

export const Container = styled.div<Props>`
  display: flex;
  flex-direction: column;
  align-items: ${(props) => (props.isCurrentUser ? "flex-end": "flex-start")};

`;

export const Origin = styled.span`
  font-size: 0.7rem;
`;

export const Message = styled.span``;
