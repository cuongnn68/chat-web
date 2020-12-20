<template>
  <div class="parent">
    <div class="child">
      <div class="name" v-on:click="$emit('click-room', roomId)">{{roomName}}</div>
      <div class="drop-down">
        <div class="id">id: #{{roomId}}</div>
        <div class="icon" v-on:click="clickSetting">
          <i class="fas fa-users-cog fa-lg"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChatRoom",
  props: [
    "roomId",
    "roomName",
  ],
  // props: {
  //   "roomId": String,
  //   "roomName": String,
  // },
  methods: {
    clickSetting() {
      this.$router.push("/room/" + this.roomId);
    }
  }
}
</script>

<style scoped>
  .parent {
  
  }
  .name:hover {
    background-color: #b15ebb;
    color: white;
    cursor: pointer;
  }
  .child {
    height: 50px;
    position: relative;
    /* display: inline; */
    display: flex;
    flex-direction: row;
  }
  .child:hover .drop-down{
    display: flex;
  }
  .name {
    /*  1 line with very long name */
    /* display: inline-block;  */
    
    white-space: nowrap; /* RM only 1 line text */ 
    flex: 1;
    width: 100%;
    line-height: 50px;
    text-align: center;
    vertical-align: middle;
  }
  .id {
    /* flex: 1; */
    /* min-width: 80px;
    max-width: 80px; */
    background-color: gray;
    color: white;
    /* line-height: 25px; */
    /* min-width: 100px; */
    text-align: center;
    padding: 0.1rem;
    font-size: 0.5em;
    /* vertical-align: middle; */
  }
  .id:hover {
    /* display: block; */
  }
  .drop-down {
    z-index: 300;

    /* left:100%; */
    right:0;
    top:0;

    position: absolute;
    display: none;
    flex: row;
    align-items: flex-end;
  }
  .icon {
    height: 50px;
    width: 50px;
    background-color: lightgray;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .icon:hover {
    background-color: gray;
    color: white;
  }
</style>