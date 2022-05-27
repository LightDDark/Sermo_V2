import User from "./User";
import Out from "./Out";

class UserData {
  async getUser(userName) {
    let response = await Out.get(
      "https://localhost:7043/api/Users/" + userName
    );
    if (response !== null) {
      return new User(response.id, response.name, response.server);
    }
    return new User();
  }

  async addUser(userName, password, nickName) {
    Out.post("https://localhost:7043/api/Users", {
      id: userName,
      password: password,
      name: nickName,
    });
    return true;
  }

  async login(userName, password) {
    let response = await Out.login("https://localhost:7043/api/Users/Login", {
      id: userName,
      password: password,
    });
    if (!response) {
      return null;
    }
    let user = new User(userName, userName, "localhost:7043", response.result);
    return user;
  }
}

const users = new UserData();

export default users;
