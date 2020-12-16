<template>
  <div class="chat">
    <SideBar v-bind:rooms="rooms" 
              v-on:choose-room="loadMessRoom" 
              v-on:showpu-joinr="popJoinRoom=true"
              v-on:showpu-creater="popNewRoom=true"
              class="side-bar"/>
    <ChatMain v-on:send-mess="sendMess" v-bind:mess="mess" class="content"/>

    <div v-if="popJoinRoom" class="popup">
      <div class="frame">
        <h2 class="frame-c">Join Room</h2>
        <label class="lb">Room Id</label>
        <input v-model="joinRoomId" class="frame-c" type="text">
        <div class="btns">
          <button v-on:click="joinRoom">Join</button>
          <button v-on:click="popJoinRoom=false">Cancel</button>
        </div>
        <h4 class="error">{{joinError}}</h4>
      </div>
    </div>

    <div v-if="popNewRoom" class="popup">
      <div class="frame">
        <h2 class="frame-c">Create Room</h2>
        <label class="lb">Name</label>
        <input v-model="newRoomName" class="frame-c" type="text">
        <div class="btns">
          <button v-on:click="newRoom">Create</button>
          <button v-on:click="popNewRoom=false">Cancel</button>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
import * as signalR from "@microsoft/signalr";
import SideBar from '../components/chat/SideBar.vue';
import ChatMain from '../components/chat/ChatMain.vue';
// import {getRooms} from "../api/userAPI.js";
import * as uapi from "../api/userAPI.js";
// import {getRoomMess} from "../api/roomAPI.js";
// import {sendRoomMess} from "../api/roomAPI.js";
import * as rapi from "../api/roomAPI.js";
import * as realTime from "../api/realTmeChat.js";
export default {
  name: "Chat",
  // TODO * leave room
  // TODO * room setting
  // TODO * chang uesr info
  components: {
    SideBar,
    ChatMain,
  },
  data() {
    return {
      rooms: [],
      users: [],
      /** @type {Array} */
      mess: [],
      typeChat: {},
      popJoinRoom:false,
      popNewRoom: false,
      joinRoomId: "",
      newRoomName: "",
      joinError: "",
      /** @param {signalR.HubConnection} */
      connection: signalR.HubConnection,
      connectionId: ""
    }
  },
  methods: {
    getData() {
      uapi.getRooms().then(res => res.json())
                    .then(val => this.rooms = val["$values"]);
      
    },
    loadMessRoom(roomId) {
      console.log("choose room: " + roomId);
      
      if(this.typeChat.roomId) {
        realTime.leaveRoom(this.typeChat.roomId, this.connectionId)
                .then(res => {
                  if(res.ok) console.log("leave real time chat room: " + this.typeChat.roomId)
                  else throw res.statusText;
                }).catch(err => console.log(err));
        
      }
      this.typeChat = {
        type: "room",
        roomId: roomId,
      };

      rapi.getRoomMess(roomId).then(res => res.json()
                .then(roomMess => {
                  console.log(roomMess["$values"]);
                  this.mess = roomMess["$values"];
                  console.log(this.mess);
                  realTime.joinRoom(roomId, this.connectionId)
                          .then(res => {
                            if(res.ok) console.log("joined room: " + roomId);
                            else throw res.text;
                          }).catch(err => console.log(err));
                }));

    },
    sendMess(mess) {
      console.log(this.typeChat["type"]);
      console.log("sendMess: " + mess);
      if(this.typeChat["type"] === "room") {
        console.log("sendRoomMess");
        const userId = JSON.parse(localStorage.getItem("userInfo"))["id"];
        console.log(this.typeChat["roomId"]);
        realTime.sendMess2Room(this.typeChat["roomId"], userId, mess)
            .then(res => {
              if(!res.ok) console.log(res.statusText)
            }).catch(e => console.log(e)); // TODO show error
      } 
    },
    newRoom() {
      if(!this.newRoomName) {
        console.log("//TODO create notice line");
        return;
      }
      uapi.newRoom(this.newRoomName)
        .then(res => {
          if(!res.ok) {
            res.json().then(err => {throw err;}).catch(console.log);
          }
          this.getData();
        }).catch(e => console.log(e));
      this.newRoomName = "";
      this.popNewRoom = false;
    },
    joinRoom() {
      console.log(this.joinRoomId);
      rapi.joinRoom(this.joinRoomId)
          .then(res => {
            if(!res.ok) this.joinError = "Cant join room " + this.joinRoomId;
            else {
              this.joinError = "";
              this.getData();
            }
          });
      this.joinRoomId = "";
      this.popJoinRoom = false; 
    },
    buildConnection() {
      this.connection = new signalR.HubConnectionBuilder()
                              .withUrl("https://" + localStorage.getItem("domain") + "/chathub")
                              .withAutomaticReconnect()
                              .build();
      this.connection
          .start()
          .then(() => this.connection
                          .invoke("getConnectionId")
                          .then(id => {
                            this.connectionId = id;
                            console.log(typeof(id));
                            console.log("connectionID: " + id);
                          })
      );
      this.connection.on("reciveMessage", newMess => { // vs function (newMess) {}
        this.mess.splice(0, 0, newMess);
      });
    }
  },
  created: function() { // RM same as created() {
    this.getData();
    this.buildConnection();
  },
  destroyed: function() {
    this.connection.stop();
  }
}
</script>

<style scoped>
  .chat {
    display: flex;

  }
  .side-bar {
    flex: 1;
    max-width: 250px;
    background-color: aliceblue;
  }
  .content {
    flex: 3;
    /* background-color: red; */
  }
  .popup {
    position: fixed;
    height: 100%;
    width: 100%;
    background-color: #ffffffcc;

    display: flex;
    justify-content: center;
    align-items: center;
  }
  .frame {
    z-index: 300;
    background-color: white;
    border-color: #b15ebb;
    border-width: 3px;
    border-style: solid;
    border-radius: 10px;
    padding: 20px;

    display:flex;
    flex-direction: column;
  }
  .frame-c {
    margin: 0 0 10px;
  }
  .btns {
    display: flex;
    justify-content: space-evenly;
  }
  .lb {
    text-align: left;
    font-size: 0.9rem;
  }
</style>