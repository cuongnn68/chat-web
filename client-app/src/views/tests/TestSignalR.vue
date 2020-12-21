<template>
  <div>
    <input v-model=mess>
    <button v-on:click="runTest">Run Test</button>
    <p> {{message}} </p>
  </div>
</template>

<script>
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import api from "../../api/index.js";
export default {
  name: "Test",
  data() {
    return {
      mess:"",
      /** @type {string} */
      message: "",
      /** @type {string} */
      connectionId: "",
      /** @type {signalR.HubConnection} */
      connection: HubConnection
    }
  },
  methods: {
    buildHub() {
      this.connection = new HubConnectionBuilder()
            .withUrl("https://" + localStorage.getItem("domain") + "/chathub")
            .withAutomaticReconnect()
            .build();
      this.connection.on("AddNewMessage", message => {

        console.log(message);
      });
      this.connection.on("reciveTest", str => {
        console.log(str);
      });
      this.connection.on("testJson", res => {
        console.log(res);
      })
    }, 
    connectHub() {
      this.connection.start();
    },
    getConnectionId() {
      const val = this.connection.invoke("GetConnectionId").then(res => console.log(res));
      console.log(val);
    },
    testMess() {
      this.connection.send("joinTestRoom");
      this.connection.invoke("messTestRoom", this.mess).then(res => console.log(res));
    },
    runTest() {
      this.connection.send("testJson");
    }
  },
  created() {
    this.buildHub();
    this.connection.start();
    // this.connection.onClose();
  },
  beforeDestroy() {
    this.connection.stop();
    this.connection.onclose(console.log("on close"));
    // this.connection
  }
}
</script>

<style>

</style>