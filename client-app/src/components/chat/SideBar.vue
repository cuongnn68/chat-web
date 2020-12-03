<template>
  <div class="parent">
    <div class="name">Chat Room</div>
    <div class="user">
      <ChatRoom roomId="11111" roomName="Room 1"/>
      <ChatRoom roomId="222" roomName="Room 2" selected="true"/>  <!-- TODO: add select solid border left -->
      <ChatRoom roomId="333" roomName="Room 3"/>
      <ChatRoom roomId="444" roomName="Room 4"/>
      <ChatRoom roomId="555" roomName="Room 16"/>
      <ChatRoom roomId="8888" roomName="Room 7"/>
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
  }
}
</script>

<style scoped>
  .parent {
    display: flex;
    flex-direction: column;

    /* background-color: green; */
  }
  .user {
    flex: 1;
    /* background-color: white; */
    overflow-y: auto;
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
</style>