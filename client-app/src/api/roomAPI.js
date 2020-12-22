import rapi from "./index.js";

export function getRoomMess(roomId) {
  return rapi.get("/api/room/" + roomId + "/message", {});
}

// export function sendRoomMess(roomId, userId, mess) {
//   return rapi.post("/api/room/" + roomId + "/message", {userId: userId, content: mess});
// }

export function sendRoomMess(roomId, userId, mess) {
  console.log(roomId);
  return rapi.post("/api/room/" + roomId + "/message", {userId: userId, content: mess});
}

export function joinRoom(roomId) {
  const userId = rapi.getUserId();
  return rapi.post("/api/room/" + roomId + "/user" , {userId})
}

/**
 * 
 * @param {number} roomId 
 */
export function getUsers(roomId) {
  return rapi.get("/api/room/" + roomId + "/user");
}

export function removeUser(roomId, userId) {
}


export function getInfo(roomId) {
  return rapi.get("/api/room/" + roomId);
}

export function update(roomId, roomName) {
  return rapi.put("/api/room/" + roomId, {name: roomName});
}

export function deleteRoom(roomId) {
  return rapi.deleteMethod("/api/room/" + roomId);
}