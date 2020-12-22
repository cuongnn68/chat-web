<template>
  <div class="parent">
    <!-- Room manager {{$route.params.id}} -->
    <div class="child">
      <div class="manager">
        <h2>Room Info</h2>
        <div class="my-input">
          <label for="room-id">Room Id</label>
          <input name="room-id" type="text" disabled v-model="roomId" >
        </div>
        <div class="my-input"> 
          <!-- FIXED when change value room it throw error -->
          <label for="room-name">Room name</label>
          <input name="room-name" type="text" v-model="roomName" v-bind:disabled="!isAdmin">
        </div>
        <div class="my-input">
          <label for="time-created">Time created</label>
          <input name="time-created" type="text" disabled v-model="timeCreated">
        </div>
        <div class="my-input list-btn">
          <button v-if="!isAdmin" v-on:click="leaveRoom" class="red-btn btn-space-right">Leave room</button>
          <button v-if="isAdmin" v-on:click="deleteRoom" class="red-btn btn-space-right">Delete room</button>
          <button v-if="isAdmin" v-on:click="updateRoom" class="grn-btn btn-space-right">Update</button>
        </div>
      </div>
      <div class="users">
        <h2>User Info</h2>
        <table class="table">
          <tr>
            <th>Id</th>
            <th style="width: 300px">Username</th>
            <th>Role</th>
            <th v-if="isAdmin"></th>
          </tr>
          <tr v-for="user in userList" v-bind:key="user.id">
            <td>{{user.id}}</td>
            <td>{{user.username}}</td>
            <td>{{user.role}}</td>
            <td v-if="isAdmin">
              <button class="red-btn" 
                      v-bind:class="{'cant-btn': user.role == 'ADMIN'}"
                      v-on:click="removeUser(user.id)">
                Remove
              </button>
            </td>
          </tr>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
// RM vscode f12 go to source code
import * as roomAPI from "../api/roomAPI.js";
import * as st from "../utils/storage.js";
import * as userAPI from "../api/userAPI.js";
export default {
  name: "RoomManager",
  data() {
    return {
      roomName: "",
      roomId: 0,
      timeCreated: "",
      isAdmin: false,
      userList: [],
    }
  },
  computed: {
    // roomId: function() {
    //   return this.$route.params.id;
    // }
  },
  methods: {
    getUser() {
      console.log(this.$route.params.id);
      console.log(roomAPI);
      roomAPI.getUsers(this.$route.params.id)
              .then(res => {
                if(!res.ok) throw "Not ok";
                res.json().then(users => {
                  // RM array + array
                  // this.userList = this.userList.concat(users["$values"]);
                  // this.userList = [...this.userList, ...users["$values"]]; 
                  // DONE add new list remove old list
                  this.userList = users["$values"];
                  
                  // FIXED always false to find compare my id with room admin id | problem: compare user id to room id
                  this.isAdmin = this.userList 
                                    //RM filter array
                                    // .filter(u => u.role === "ADMIN")
                                    .some(u => (u.role === "ADMIN") && (u.id == st.getUser().id));
                                    // .map(u => u.id)
                                    // .includes(this.$route.params.id);
                  console.log("isadmin: " + this.isAdmin);
                });
              }).catch(e => console.log(e));
    },
    getRoomInfo() {
      roomAPI.getInfo(this.$route.params.id)
            .then(res => {
              if(!res.ok) throw "not ok";
              res.json().then(val => {
                this.roomId = val.id;
                this.roomName = val.name;
                this.timeCreated = val.dayCreated;
              }).catch(e => {throw e;});
            }).catch(e => console.log(e))
    },
    newNoti(content, color) {
      this.$emit("newNoti", {content, type: color});
    },
    removeUser(userId) {
      console.log(userId);
      let user = this.userList.find(u => u.id == userId);
      if(user.role === "ADMIN") {
        this.newNoti("Cant remove admin", "red");
        return;
      }
      roomAPI.removeUser(this.roomId, userId)
            .then(res => {
              if(res.ok){
                let i = this.userList.findIndex(u => u.id == userId);
                if(i !== -1) this.userList.splice(i,1);
                this.newNoti("Remove user", "green");

              } else {
                res.json().then(val => {
                  if(!res.ok) this.newNoti(val.error, "red");
                }).catch(e => {throw e;})
              }
            })
            .catch(e => {
              console.log(e);
              this.newNoti("ERROR", "red");
            });     
    },
    deleteRoom() {
      console.log(this.roomId);
      roomAPI.deleteRoom(this.roomId)
            .then(res => {
              if(res.ok) {
                this.$router.push("/chat");
                this.newNoti("Room deleted", "green");
              } else {
                res.json().then(val => this.newNoti(val.error, "red"));
              }
            }).catch(e => {
              console.log(e);
              this.newNoti("ERROR cant delete room", "red");
            });
    },
    leaveRoom() {
      // console.log(this.roomId);
      // console.log(st.getUser().id);
      userAPI.leaveRoom(this.roomId, st.getUser().id)
            .then(res => {
              if(res.ok) {
                this.$router.push("/chat");
                this.newNoti("Leaved room " + this.roomName, "green");
              } else {
                res.json().then(e => this.newNoti(e.error, "red"));
              }
            })
            .catch(e => {
              console.log(e);
              this.newNoti("ERROR cant leave room", "red");
            });
    },
    updateRoom() {
      //TODO use (create) other api that check uer is admin or not
      roomAPI.update(this.roomId, this.roomName)
            .then(res => {
              if(res.ok) {
                this.$router.go();
                this.newNoti("Chat room update", "green");
              } else {
                res.json()
                  .then(val => this.newNoti(val.error, "red"))
                  .catch(e => {throw e;});
              }
            })
            .catch(e => {
              console.log(e);
              this.newNoti("ERROR cant update room", "red");
            })
    }
  },
  created() {
    this.getUser();
    this.getRoomInfo();
    console.log("where mah log")
    console.log(this.userList.filter(u => u.role === "ADMIN"));
  }
}
</script>

<style scoped>
  .child {
    /* height: 100%;
    width: 100%; */
    overflow: hidden;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;
  }
  .child > div {
    /* max-height: 100%; */
    padding: 30px;
  }
  .list-btn {
    flex-direction: row;
  }
  .btn-space-right {
    margin-right: 10px;
  }
  .cant-btn {
    opacity: 0.5;
    cursor: not-allowed;
  }
  table {
    margin-bottom: 10px;
  }
</style>