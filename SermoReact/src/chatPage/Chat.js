import ContactCard from "./ContactCard";
import SubChat from "./SubChat";
import MessageForm from "./MessageForm";
import Col from "react-bootstrap/Col";

function Chat(props) {
  const [activeContact, setActiveContact] = props.active;
  const user = props.user;
  return (
    <Col>
      <ContactCard contact={activeContact} />
      <SubChat
        messages={user.getMessages(activeContact.getName())}
        userName={user.getName()}
      />
      <MessageForm
        id="inside-card"
        active={[activeContact, setActiveContact]}
        user={user}
      />
    </Col>
  );
}

export default Chat;
