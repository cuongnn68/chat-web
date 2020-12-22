import rapi from "./index.js"

export function searchUser(keyword) {
  return rapi.get("/api/search/user", {keyword});
}

export function searchRoom(keyword) {
  return rapi.get("/api/search/room", {keyword});
}