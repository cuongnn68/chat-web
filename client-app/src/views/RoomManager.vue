<template>
  <div class="parent">
    Room manager {{$route.params.id}}
    <div class="child">
      <div class="manager">
        <h2>Room Info</h2>
        <div class="my-input">
          <label for="room-id">Room Id</label>
          <input name="room-id" type="text">
        </div>
        <div class="my-input">
          <label for="room-name">Room name</label>
          <input name="room-name" type="text">
        </div>
        <div class="my-input">
          <button>Leave room</button>
          <button>Delete room</button>
        </div>
      </div>
      <div class="users">
        <h2>User Info</h2>
        <table class="table">
          <tr>
            <th>Id</th>
            <th>Username</th>
            <th>Role</th>
            <!-- <th>4st</th> -->
          </tr>
          <tr>
            <td>most the dog</td>
            <td>most the dog</td>
            <td>most the dog</td>
            <td>most the dog</td>
          </tr>
          <tr v-for="user in userList" v-bind:key="user.id">
            <td>{{user.id}}</td>
            <td>{{user.username}}</td>
            <td>{{user.role}}</td>
            <button>Remove</button>
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
      isAdmin: false,
      userList: [
        {
          username: "test1",
          id: 1,
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
      ],
    }
  },
  computed: {
    roomId: function() {
      return this.$route.params.id;
    }
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
                  this.userList = users["$values"];
                });
              }).catch(e => console.log(e));
    }
  },
  created() {
    this.getUser();
    // console.log(this.roomId);
  }
}
</script>

<style>
  .child {
    height: 100%;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
  }
</style>