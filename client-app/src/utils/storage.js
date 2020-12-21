export function getUser() {
  let info = localStorage.getItem("userInfo");
  return JSON.parse(info);
}