import toastr from "./components/toast";
export default function initApp(){
    var token = localStorage.getItem('token');
    var isAuth = token != null && token != '' && (typeof token) === 'string';
    var isSignoutPage = window.location.href.includes('signout');

    if(isAuth == false){

        if(isSignoutPage == false){
            toastr.info('Sessão encerrada');
            setTimeout(()=>{window.location = 'login2.html'}, 400);
        }
        
    }
}