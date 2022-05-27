import "./subChat.css";
import PrintLog from "./PrintLog";
import { Row } from "react-bootstrap";

function SubChat(props) {
  const messages = props.messages;
  const userName = props.userName;
  const description = function () {
    if (messages.length) {
      return <PrintLog messages={messages} userName={userName} />;
    }
    return <h6 className="message">no messages</h6>;
  };

  return <Row className="chat">{description()}</Row>;
}
export default SubChat;
