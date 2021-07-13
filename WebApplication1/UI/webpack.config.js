"devServer": {
  "historyApiFallback": true,
  "proxy": {
    "/api": {
      "target" : "http://localhost:44343",
      "secure": false
    }
  }
}