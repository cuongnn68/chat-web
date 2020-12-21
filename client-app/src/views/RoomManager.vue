<template>
  <div class="parent">
    Room manager {{$route.params.id}}
    <div class="child">
      <div class="manager">
        <h2>Room Info</h2>
        <div class="my-input">
          <label for="room-id">Room Id</label>
          <input name="room-id" type="text" disabled v-model="this.roomId" >
        </div>
        <div class="my-input"> 
          <!-- FIXME when change value room it throw error -->
          <label for="room-name">Room name</label>
          <input name="room-name" type="text" v-model="this.roomName" v-bind:disabled="!isAdmin">
        </div>
        <div class="my-input">
          <label for="time-created">Time created</label>
          <input name="time-created" type="text" disabled v-model="this.timeCreated">
        </div>
        <div class="my-input list-btn">
          <button v-if="!isAdmin" class="red-btn btn-space-right">Leave room</button>
          <button v-if="isAdmin" class="red-btn btn-space-right">Delete room</button>
          <button v-if="isAdmin" class="grn-btn btn-space-right">Update</button>
        </div>
      </div>
      <div class="users">
        <h2>User Info</h2>
        <table class="table">
          <tr>
            <th>Id</th>
            <th>Username</th>
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
export default {
  name: "RoomManager",
  data() {
    return {
      roomName: "",
      roomId: 0,
      timeCreated: "",
      isAdmin: false,
      userList: [
        {
          username: "test1",
          id: 1,
          role: "ADMIN"
        },
        {
          username: "test2",
          id: 2,
        },
        {
          username: "test3",
          id: 3,

        },
        {
          username: "test4",
          id: 4,
        },
        {
          username: "test1",
          id: 6,
        },
        {
          username: "test2",
          id: 5,
        },
        {
          username: "test3",
          id: 7,

        },
        {
          username: "test4",
          id: 8,
        },
      ],
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
                  console.log(users);
                  // this.userList = users["$values"];
                  // this.userList = this.userList.concat(users["$values"]);
                  this.userList = [...this.userList, ...users["$values"]];
                  console.log(this.userList);
                  this.isAdmin = this.userList // FIXME always false to find compare my id with room admin id
                                    // .filter(u => u.role === "ADMIN")
                                    .some(u => u.role === "ADMIN" && u.id == this.$route.params.id);
                                    // .map(u => u.id)
                                    // .includes(this.$route.params.id);
                  console.log("isadmin" + this.isAdmin);
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
    removeUser(userId) {
      console.log(userId);
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