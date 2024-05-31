<template>
  <div class="home d-flex flex-column align-items-center justify-content-center">
    <div class="p-5 card align-items-center shadow rounded elevation-3">
      <img src="https://www.pngkey.com/png/full/114-1149878_setting-user-avatar-in-specific-size-without-breaking.png" alt="avatar" />
      <h1>Welcome to Vue 3 Client!</h1>
    </div>

    <TodoForm />
    <div class="col-12 d-flex flex-column justify-content-start p-3">
      <p>Current Tasks:</p>
        <ol>
          <li v-for="t in todos" :key="t.id" class="todo-container d-flex justify-content-between">
            <TodoCard :todo="t" />
          </li>
        </ol>
    </div>
  </div>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import TodoCard from "../components/Todos/TodoCard.vue";
import TodoForm from "../components/Todos/TodoForm.vue";

export default {
  setup() {
    return {
      todos: computed(() => AppState.todos),
      account: computed(() => AppState.account),
    }
  },
  components: {
    TodoForm, TodoCard,
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: auto;
  gap: 20px;
  place-content: center;
  user-select: none;
    img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }

}
</style>
