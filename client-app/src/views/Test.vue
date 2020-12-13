<template>
  <div>
    <button v-on:click="getConnectionId">Run Test</button>
    <p> {{message}} </p>
  </div>
</template>

<script>
import {HubConnectionBuilder} from "@microsoft/signalr";
import api from "../api/index.js";
export default {
  name: "Test",
  data() {
    return {
      message: "",
      connectionId: "",
      connection: HubConnectionBuilder
    }
  },
  methods: {
    buildHub() {
      this.connection = new HubConnectionBuilder()
            .withUrl("https://" + localStorage.getItem("domain") + "/chathub")
            .withAutomaticReconnect()
            .build();
      this.connection.on("AddNewMessage", message => {
        // TODO: add message to list
        console.log("mess go here");
      })
    }, 
    connectHub() {
      this.connection.start();
    },
    getConnectionId() {
      const val = this.connection.invoke("getConnectionId")
      console.log(val);
    }
  },
  created() {
    this.buildHub();
    this.connection.start();
    // this.connection.onClose();
  },
  beforeDestroy() {
    this.connection.onClose(() => console.log("fuck this shit"));
    // this.connection
  }
}
</script>

<style>

</style>