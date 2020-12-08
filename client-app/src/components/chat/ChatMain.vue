<template>
  <div class="parent">
    <div class="child">
        <div class="chat-content">
        <ChatMessage username="nncuong" time="12:00" message="First message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry short message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
        <ChatMessage username="nncuong" time="12:00" message="verry long long long long long long long long long long long long long long long long message"/>
      </div>
      <div class="chat-input">
        <input ref="ok" v-model="mess" class="input" type="text">
        <button v-on:click="send" class="button">Send</button>
      </div>
    </div>
  </div>
</template>

<script>
import ChatMessage from "./ChatMessage.vue";
export default {
  name: "ChatMain",
  components: {
    ChatMessage,
  },
  data() {
    return {
      mess: ""
    }
  },
  methods: {
    send() {
      // this.$el.querySelector("#input"); //RM use to like normal (need id="")
      console.log(this.mess);
      this.$emit("send-mess", this.mess);

      // this.$refs.ok.value = ""; //RM clear without binding
      this.mess = "";
    }
  }
}
</script>

<style scoped>
  .parent {
    
  }
  .child {
    height: 100%;
    display: flex;
    flex-direction: column;

  }
  .chat-content {
    /* max-height: 100%; */
    overflow: auto;
    flex:1 1;
    display: flex;
    flex-direction: column-reverse;
    justify-content: flex-start;
    padding:30px;
    /* align-items: flex-start; */
    /* background-color: blue; */
  }
  .chat-input {
    display: flex;
    min-height: 35px;
    height: 35px;
    /* background-color:red; */
    justify-content: flex-end;
    padding: 10px;
    box-shadow: 0 -3px 3px lightgray;
  }
  .input {
    flex: 1;
    /* border-radius: 20px; */
    margin-right: 10px;
  }

</style>