import SideBar from "./SideBar";
import Chat from "./Chat";
import { HubConnectionBuilder } from "@microsoft/signalr";

import { useState, useEffect } from "react";
import "./subChat.css";
import { Container, Row, Col } from "react-bootstrap";

function MainChat(props) {
  const user = props.user;
  const [activeContact, setActiveContact] = useState(null);
  // const [connection, setConnection] = useState(null);
  const [contacts, setContacts] = useState([]);

  // useEffect(() => {
  //   const newConnection = new HubConnectionBuilder()
  //     .withUrl("https://localhost:7043/MsgHub")
  //     .withAutomaticReconnect()
  //     .build();

  //   setConnection(newConnection);
  // }, []);

  useEffect(() => {
    async function getContacts() {
      let c = await user.getContacts();
      setContacts(c);
    }
    getContacts();
  }, [user]);

  // useEffect(() => {
  //   if (connection) {
  //     connection
  //       .start()
  //       .then((result) => {
  //         console.log("Connected!");

  //         connection.on("ReceiveMessage", (message) => {
  //           const l = logs.getLog(message.from, message.to);
  //           l.newMessage("text", message.content, message.from);
  //           if (
  //             activeContact[0] &&
  //             l.userNames === activeContact[1].userNames
  //           ) {
  //             const temp = activeContact[0];
  //             setActiveContact([temp, l]);
  //           }
  //         });
  //       })
  //       .catch((e) => console.log("Connection failed: ", e));
  //   }
  // }, [connection, activeContact]);

  return (
    <Container fluid>
      <Row>
        <Col sm={2}>
          <SideBar
            contacts={contacts}
            self={user}
            active={[activeContact, setActiveContact]}
          />
        </Col>
        {activeContact ? (
          <Chat active={[activeContact, setActiveContact]} user={user} />
        ) : (
          <Col className="p-3 mb-2 bg-secondary text-white"></Col>
        )}
      </Row>
    </Container>
  );
}
export default MainChat;
