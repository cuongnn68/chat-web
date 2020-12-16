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