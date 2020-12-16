import rapi from "./index.js";

/**
 * 
 * @param {string} roomId 
 * @param {string} connectionId 
 */
export function joinRoom( roomId, connectionId) {
  return rapi.post("/api/chat/room/" + roomId + "/" + connectionId);
}

/**
 * 
 * @param {string} roomId 
 * @param {string} connectionId 
 */
export function leaveRoom( roomId, connectionId) {
  return rapi.deleteMethod("/api/chat/room/" + roomId + "/" + connectionId);
}

/**
 * 
 * @param {string} roomId 
 * @param {string} userId 
 * @param {string} mess 
 */
export function sendMess2Room(roomId, userId, mess) {
  return rapi.post("/api/chat/room/" + roomId + "/message", {userId: userId, content: mess});
}