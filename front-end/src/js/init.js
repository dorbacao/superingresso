import toastr from 'toastr';
export default function initApp(){
    var login = localStorage.getItem('login');
    var isAuth = login != null && login != '' && typeof login === 'string';
    var isSignoutPage = window.location.href.includes('signout');

    if(isAuth == false){

        if(isSignoutPage == false){
            toastr.info('Sessão encerrada');
            setTimeout(()=>{window.location = 'login.html'}, 1000);
        }
        
    }
}