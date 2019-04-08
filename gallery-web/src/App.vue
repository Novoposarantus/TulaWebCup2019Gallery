<template>
  <div id="app">
  <div class="content">
    <header>
      <div>
        <label class="main-label">Галерея</label>
      </div>
      <nav>
        <ul class="list-group">
          <router-link :to='"/login"'
                      tag="li"
                      class="list-group-item">
            <a>Авторизация</a>
          </router-link>
        </ul>
      </nav>
    </header>
    <main>
      <router-view class="router"></router-view>
    </main>
  </div>
  <footer>
    <span class="footer-text">
      TulaWebCup2019 Gallery
    </span>
  </footer>
  </div>
</template>

<script>

export default {
  name: 'app',
}
</script>

<style>
* {
  margin: 0;
  padding: 0;
}
html,
body {
  height: 100%;
}
#app {
  display: flex;
  flex-direction: column;
  height: 100%;
}
header{
  display: flex;
  align-items: center;
  padding: 0 20px;
  background-color: rgb(29, 29, 29);
  height: 50px;
  color: white;
}
.main-label{
  font-size: 2em;
}
nav{
  margin-left: auto;
}
nav ul li{
  list-style-type: none;
}
nav ul li a{
  color: white;
}
.router-link-exact-active a{
  color: rgb(103, 103, 255);
}
nav ul{
  margin-left: 0;
  padding-left: 0;
}
.content {
  flex: 1 0 auto;
  display: flex;
  flex-direction: column;
}
footer {
  flex: 0 0 auto;
  display: flex;
  height: 50px;
  width: 100%;
  justify-content: center;
  align-items: center;
  background-color: rgb(29, 29, 29);
  color: white;
}
main{
  display: flex;
}
main,
.router{
  flex: 1 1;
}
</style>
