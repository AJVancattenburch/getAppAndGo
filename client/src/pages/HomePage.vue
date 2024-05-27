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
  </div>
</template>

<script>
import { computed } from "vue";
import { todosService } from "../services/TodosService.js";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";

export default {
  setup() {
    
    async function getAllTodos() {
      try {
        await todosService.getAllTodos();
      } catch (error) {
        logger.error(error);
      }
    }

    return {
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
