<template>
  <div class="chat">
    <SideBar v-bind:rooms="rooms" v-on:choose-room="loadMessRoom" class="side-bar"/>
    <ChatMain v-on:send-mess="sendMess" class="content"/>
  </div>
</template>

<script>
import SideBar from '../components/chat/SideBar.vue';
import ChatMain from '../components/chat/ChatMain.vue';
import {getRooms} from "../api/userAPI.js";
import {getRoomMess} from "../api/roomAPI.js";
import {sendRoomMess} from "../api/roomAPI.js";
export default {
  name: "Chat",
  components: {
    SideBar,
    ChatMain,
  },
  data() {
    return {
      rooms: [],
      users: [],
      typeChat: {}
    }
  },
  methods: {
    getData() {
      getRooms().then(res => res.json())
              .then(val => this.rooms = val["$values"]);

    },
    loadMessRoom(roomId) {
      console.log("choose room " + roomId);
      this.typeChat = {
        type: "room",
        roomId: roomId,
      }
      getRoomMess(roomId).then(res => res.json()
                                    .then(mess => console.log(mess)));
                                    //TODO add message
    },
    sendMess(mess) {
      // console.log(mess);
      if(this.typeChat["type"] === "room") sendRoomMess(mess);
    },
    sendRoomMess(mess) {
      console.log("get herer")
      const userId = JSON.parse(localStorage.getItem("userInfo"))["id"];
      console.log(this.typeChat["roomId"]); // FIXME how the fuck this is messeage //FIXME how the actual fuck
      sendRoomMess(this.typeChat["roomId"], userId, mess)
          .then(res => {
            if(!res.ok) console.log(res.statusText)
          }).catch(e => console.log(e)); // TODO show error
    }
  },
  created: function() { // RM same as created() {
    this.getData();
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
</style>