<template>
  <div class="parent">
    <div class="child">
      <p class="info">
        <span class="username">{{username}}:</span>  <span class="time">{{time}}</span>
      </p>
      <p class="message">{{message}}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChatMessage",
  props: {
    username: String,
    time: String,
    message: String,
  }
}
</script>

<style scoped>
  /* .parent {

  } */
  .child {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    margin: 0 0 15px;
  }
  .message {
    color: white;
    background-color: #b15ebb;
    text-align: left;
    padding: 10px;
    border-radius: 20px;
  }
  .info {
    /* align-self: stretch; */
    /* text-align: center; */
    vertical-align: middle;
    display: flex;
    /* justify-content: space-between; */
    font-size: 17px;

    align-items: flex-end;
    /* TODO:
    put the fucking time lower
    maybe because i use reverse column, 
    but why solum affect row,
    i mean fukc this shit*/
    /* ?: what?? */

  }
  .username {
    font-weight: 600;
  }
  .time {
    font-size: 80%;
    font-weight: normal;
  }
</style>