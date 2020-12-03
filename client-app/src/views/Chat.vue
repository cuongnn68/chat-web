<template>
  <div class="chat">
    <SideBar class="side-bar"/>
    <ChatMain class="content"/> 
  </div>
</template>

<script>
import SideBar from '../components/chat/SideBar.vue';
import ChatMain from '../components/chat/ChatMain.vue';
export default {
  name: "Chat",
  components: {
    SideBar,
    ChatMain,
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