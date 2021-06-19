import styled from "styled-components";

interface Props {
  isIn: boolean;
}

const MessageCard = styled.div`
  display: flex;
  padding: 0.3rem 3rem 0.3rem 0.3rem;
  border-radius: 10%;
  margin-bottom: 3px;
`

export const MessageInCard = styled(MessageCard)`
  background-color: #ffeede;
`;

export const MessageOutCard = styled(MessageCard)`
  background-color: #e9f7df;
`;

export const Container = styled.div<Props>`
  display: flex;
  flex-direction: column;
  align-items: ${(props) => (props.isIn ? "flex-start" : "flex-end")};
`;

export const Origin = styled.span`
  font-size: 0.7rem;
`;

export const Message = styled.span`

`;
