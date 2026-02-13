
{
    Usuario u = new Usuario();
    Administrador a = new Administrador();

    Console.WriteLine("Usuário autenticou? " + u.Autenticar("472"));
    Console.WriteLine("Administrador autenticou? " + a.Autenticar("admin"));
}
