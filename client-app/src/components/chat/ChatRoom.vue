<template>
  <div class="parent">
    <div class="child">
      <div class="id">#{{roomId}}</div>
      <div class="name">{{roomName}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChatRoom",
  props: {
    "roomId": String,
    "roomName": String,
  }
}
</script>

<style scoped>
  .parent {
  
  }
  .parent:hover {
    background-color: #b15ebb;
    color: white;
    cursor: pointer;
  }
  .child {
    height: 50px;
    display: flex;
    flex-direction: row;
  }
  .id {
    flex: 1;
    min-width: 80px;
    max-width: 80px;
    line-height: 50px;
    text-align: center;
    vertical-align: middle;

    display: none;
  }
  .name {
    flex: 3;
    line-height: 50px;
    text-align: center;
    vertical-align: middle;
  }
</style>