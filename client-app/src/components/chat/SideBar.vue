<template>
  <div class="parent">
    <div class="name"> <p>Chat Room #{{roomId}}</p></div>
    <div class="options">
      <div v-on:click="$emit('showpu-joinr')" class="join-rm">Join</div>
      <div v-on:click="$emit('showpu-creater')" class="create-rm">Create</div>
    </div>
    <div class="user">
      <!-- <p v-for="room in roomss" 
                :key="room.id" 
                v-bind:roomId="room.id" 
                v-bind:roomName="room.name">{{room.name}}</p> -->
      <!-- RM click on component use v-on:click.native -->
      <ChatRoom v-on:click-room="choosed"
                v-for="room in rooms" 
                v-bind:key="room.id" 
                v-bind:roomId="room.id" 
                v-bind:roomName="room.name"></ChatRoom>

      <!-- <ChatRoom roomId="222" roomName="Room 2" selected="true"/>  //TODO: add select solid border left -->
      <!-- <ChatRoom roomId="11111" roomName="Room 1"/>
      <ChatRoom roomId="333" roomName="Room 3"/>
      <ChatRoom roomId="444" roomName="Room 4"/>
      <ChatRoom roomId="555" roomName="Room 16"/>
      <ChatRoom roomId="8888" roomName="Room 7"/> -->
      <!-- <ChatRoom roomId="9999" roomName="Room 18888"/>
      <ChatRoom roomId="11111" roomName="Room 1"/>
      <ChatRoom roomId="222" roomName="Room 2"/>
      <ChatRoom roomId="333" roomName="Room 3"/>
      <ChatRoom roomId="444" roomName="Room 4"/>
      <ChatRoom roomId="555" roomName="Room 16"/>
      <ChatRoom roomId="8888" roomName="Room 7"/>
      <ChatRoom roomId="9999" roomName="Room 18888"/> -->
    </div>
    <div class="name">User</div>
    <div class="chat-room">
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
      <ChatUser userId="6969" userName="Nana"/>
    </div>
  </div>
</template>

<script>
import ChatRoom from "./ChatRoom.vue";
import ChatUser from "./ChatUser.vue"
export default {
  name: "SideBar",
  components: {
    ChatRoom,
    ChatUser
  },
  props: [
    "rooms"
  ],
  computed: {
    roomss() {
      return this.rooms;
    }
  },
  methods: {
    choosed(roomId) {
      this.roomId = roomId;
      this.$emit('choose-room', roomId);
    },
  },
  data() {
    return {
      roomId: "",
    }
  }
}
</script>

<style scoped>
  .parent {

    display: flex;
    flex-direction: column;

    box-shadow: 1px 0px 1px lightgray;
    /* background-color: green; */


  }
  .user {
    flex: 1;
    /* background-color: white; */
    /* overflow-y: hidden; */
    overflow-y: scroll;
    /* overflow-x: visible; */
    display: flex;
    flex-direction: column;
    border-bottom: solid;
    border-bottom-color: #b15ebb;
    border-bottom-width: 1px;
  }
  .chat-room {
    flex: 1;
    /* background-color: black; */
    overflow-y: auto;
  }
  .name {
    min-height: 40px;
    max-height: 40px;
    line-height: 40px;
    text-align: center;
    vertical-align: middle;
    color: #b15ebb;
    /* background-color:white; */
    font-weight: 700;
    /* border-bottom: solid; */

    box-shadow: 0px 3px 3px lightgray;
    
  }
  .options{
    display:flex;
    height: 30px;
    /* background-color: white; */
    color:#b15ebb;
    cursor: pointer;
    box-shadow: 0 1px 1px lightgray;
  }
  .join-rm {
    flex-grow: 1;
    line-height: 30px;
  }
  .join-rm:hover {
    background-color: dodgerblue;
    color: white;
  }
  .create-rm {
    flex-grow: 1;
    line-height: 30px;
  }
  .create-rm:hover{
    background-color: limegreen;
    color: white;
  }
</style>