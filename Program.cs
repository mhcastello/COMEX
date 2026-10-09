using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

Dictionary<string, string> clientes = new Dictionary<string, string>();

Dictionary<string, float> produtos = new Dictionary<string, float>();

Dictionary<string, Dictionary<string, int>> carrinho = new Dictionary<string, Dictionary<string, int>>();

void ExibirMenuDeOpcoes()
{
    int opcao = 0;

    while (opcao != -1)
    {
        Console.Clear();
        Console.WriteLine("Digite  1 - Cadastrar Cliente\nDigite  2 - Listar Clientes\nDigite  3 - Cadastrar Produto");
        Console.WriteLine("Digite  4 - Alterar preco de Produto\nDigite  5 - Adicionar produto no carrinho");
        Console.WriteLine("Digite  6 - Fechar Compra\nDigite -1 - Encerrar o Menu");
        if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1; 
        Console.WriteLine($"Sua opcao escolhida foi {opcao}\n");

        switch (opcao)
        {
            case 1:
                CadastraCliente();
                break;

            case 2:
                ListaClientes();
                break;

            case 3:
                CadastraProduto();
                break;

            case 4:
                AjustarPrecoDeProduto();
                break;

            case 5:
                AdicionaProdutoNoCarrinho();
                break;

            case 6:
                FechaCompra();
                break;

            default:
                opcao = -1;
                break;

        }
    }

    Console.WriteLine("COMEX FINALIZADO!");
}

void CadastraCliente()
{
    string cpf, nome; 

    Console.WriteLine("Digite as informações do cliente: CPF e Nome:");
    cpf = Console.ReadLine()!;
    nome = Console.ReadLine()!;

    if (clientes.ContainsKey(cpf))
    {
        Console.WriteLine("Esse cpf já está cadastrado para alguma pessoa");
    }
    else
    {
        clientes.Add(cpf, nome);
        Console.WriteLine($"Usuario {nome} ({cpf}) cadastrado com sucesso.");
    }
    TravaConsole();
}

void ListaClientes()
{
    foreach (string c in clientes.Keys)
    {
        Console.WriteLine($"CPF {c} e Nome {clientes[c]}");
    }
    TravaConsole();
}

void CadastraProduto()
{
    string nomeProduto;
    float preco;

    Console.WriteLine("Informe o nome do produto e o seu preco");

    nomeProduto = Console.ReadLine()!;
    if (!float.TryParse(Console.ReadLine(), out preco)) Console.WriteLine("Preco invalido.");
    else
    {
        if (produtos.ContainsKey(nomeProduto))
        {
            Console.WriteLine("Esse produto ja foi cadastrado.");
        }
        else
        {
            produtos.Add(nomeProduto, preco);
            Console.WriteLine($"Produto {nomeProduto} cadastrado com sucesso.");
        }

    }

    TravaConsole();
}

void AjustarPrecoDeProduto()
{
    string produto;
    float preco = 0.0f;

    Console.WriteLine("Digite o nome do produto que deve ter o preço alterado");
    produto = Console.ReadLine()!;

    if (!produtos.ContainsKey(produto))
    {
        Console.WriteLine("Produto não cadastrado no sistema.");
    }else
    {
        Console.WriteLine("Digite o novo preço do produto:");
        if (!float.TryParse(Console.ReadLine()!, out preco)) Console.WriteLine("preco invalido.");
        else
        {
            produtos[produto] = preco;
            Console.WriteLine("Preco alterado com sucesso.");
        }
        
    }
    TravaConsole();
}

void AdicionaProdutoNoCarrinho()
{

    string cpfCliente, nomeProduto;

    Console.WriteLine("Digite o cpf do cliente que está comprando");
    cpfCliente = Console.ReadLine()!;

    if (!clientes.ContainsKey(cpfCliente))
    {
        Console.WriteLine("Usuario nao cadastrado");
    }
    else
    {
        if (!carrinho.ContainsKey(cpfCliente)) carrinho[cpfCliente] = new Dictionary<string, int>();

        Console.WriteLine("Informe o produto que deseja comprar:");
        nomeProduto = Console.ReadLine()!;

        if (!carrinho[cpfCliente].ContainsKey(nomeProduto))
        {
            carrinho[cpfCliente].Add(nomeProduto, 1);
        }else
        {
            carrinho[cpfCliente][nomeProduto]++;
        }

        Console.WriteLine($"{nomeProduto} adicionado no carrinho de {clientes[cpfCliente]}. Numero de {nomeProduto}s comprados = {carrinho[cpfCliente][nomeProduto]}");


    }
    TravaConsole();
}

void FechaCompra() 
{
    string cpfCliente, numeroCartao;
    float somaCarrinho = 0.0f;
    
    Console.WriteLine("Digite o cpf do cliente que quer fechar o carrinho:");
    cpfCliente = Console.ReadLine()!;

    if (!clientes.ContainsKey(cpfCliente))
    {
        Console.WriteLine("Cliente nao cadastrado.");
    }else
    {
        if (!carrinho.ContainsKey(cpfCliente)) carrinho[cpfCliente] = new Dictionary<string, int>();
        foreach (string p in carrinho[cpfCliente].Keys)
        {
            Console.WriteLine($"Produto {p} com a quantidade {carrinho[cpfCliente][p]}.");
            somaCarrinho += produtos[p] * carrinho[cpfCliente][p];
        }
    }

    Console.WriteLine($"O valor final da compra eh {somaCarrinho}. Insira o numero do cartao do cliente:");

    numeroCartao = Console.ReadLine()!;
    Console.WriteLine($"Efetuado pagamento no valor de {somaCarrinho} no cartao {numeroCartao}");

    TravaConsole();
}

void TravaConsole()
{
    Console.WriteLine("Digite alguma tecla para retornar ao menu:");
    Console.ReadKey();
}

ExibirMenuDeOpcoes();
