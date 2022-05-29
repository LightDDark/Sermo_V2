import ContactCard from "./ContactCard";
import SubChat from "./SubChat";
import MessageForm from "./MessageForm";
import Col from "react-bootstrap/Col";
import { useEffect, useState } from "react";

function Chat(props) {
    const [activeContact, setActiveContact] = props.active;
    const [msgs, setMsgs] = useState(null);
    const user = props.user;
   
    useEffect(()=> {
        async function getMsgs() {
        const msgs = await user.getMessages(activeContact.getName());
        setMsgs(msgs);
    }
        getMsgs();
    },[]);
    return (
  
        <Col>
            <script src="~/lib/dist/browser/signalr.js"></script>
            <script src="~/js/ws.js"></script>
      <ContactCard contact={activeContact} />
      <SubChat
        messages={msgs}
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
