<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="home-card p-5 card align-items-center shadow rounded elevation-3">
      <img src="https://www.pngkey.com/png/full/114-1149878_setting-user-avatar-in-specific-size-without-breaking.png" alt="avatar" />
      <h1>Welcome to your Vue 3 App!</h1>
      <p>Here are your todos:</p>
      <ul>
        <li v-for="todo in todos" :key="todo.id">{{ todo.description }}</li>
      </ul>
    </div>

    <div class="home-card p-5 card align-items-center shadow rounded elevation-3">
      <h2>Create a new todo:</h2>
      <form @submit.prevent="createTodo">
        <input v-model="todo.title" type="text" placeholder="Enter a title" />
        <input v-model="todo.description" type="text" placeholder="Enter a description" />
        <button type="submit" class="btn btn-primary">Create</button>
      </form>
    </div>
  </div>
</template>

<script>
import { computed, ref } from "vue";
import { todosService } from "../services/TodosService.js";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const todo = ref({
      description: "",
      completed: false
    });
    
    async function getAllTodos() {
      try {
        await todosService.getAllTodos();
      } catch (error) {
        logger.error(error);
      }
    }

    async function createTodo() {
      try {
        await todosService.createTodo(todo.value);
        await getAllTodos();
      } catch (error) {
        logger.error(error);
      }
    }

    return {
      todo,
      createTodo,
      getAllTodos,
      todos: computed(() => AppState.todos)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
