
(function(d, s, id){
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
  }(document, 'script', 'facebook-jssdk'));

  
window.fbAsyncInit = function() {
  FB.init({
    //appId      : '843702737646881',
    appId      : '3612394438985409',
    
    cookie     : true,
    xfbml      : true,
    version    : 'v20.0'
  });

  FB.AppEvents.logPageView();   
};
  
