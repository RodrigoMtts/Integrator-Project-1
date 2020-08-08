-- MySQL dump 10.17  Distrib 10.3.22-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: BomGosto
-- ------------------------------------------------------
-- Server version	10.3.22-MariaDB-0+deb10u1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Contato`
--

DROP TABLE IF EXISTS `Contato`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Contato` (
  `idContato` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(80) DEFAULT NULL,
  `email` varchar(80) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `mensagem` varchar(500) DEFAULT NULL,
  `data` datetime DEFAULT NULL,
  PRIMARY KEY (`idContato`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contato`
--

LOCK TABLES `Contato` WRITE;
/*!40000 ALTER TABLE `Contato` DISABLE KEYS */;
INSERT INTO `Contato` VALUES (9,'Luciano Pereira Gomes da Silva','Luciano@gmail.com','21 96766-3271','Gostaria de comprar muitos bolos, mas me falta dinheiro','2020-07-02 17:07:36');
/*!40000 ALTER TABLE `Contato` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ItensPedido`
--

DROP TABLE IF EXISTS `ItensPedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ItensPedido` (
  `idPedido` int(11) DEFAULT NULL,
  `idProduto` int(11) DEFAULT NULL,
  KEY `idPedido` (`idPedido`),
  KEY `idProduto` (`idProduto`),
  CONSTRAINT `ItensPedido_ibfk_1` FOREIGN KEY (`idPedido`) REFERENCES `Pedido` (`idPedido`),
  CONSTRAINT `ItensPedido_ibfk_2` FOREIGN KEY (`idProduto`) REFERENCES `Produto` (`idProduto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ItensPedido`
--

LOCK TABLES `ItensPedido` WRITE;
/*!40000 ALTER TABLE `ItensPedido` DISABLE KEYS */;
INSERT INTO `ItensPedido` VALUES (1,2),(1,1),(1,9),(2,10),(2,6),(2,11),(2,12),(3,4),(4,2),(4,3),(4,9),(5,1),(5,11),(5,10),(5,5),(6,1),(7,2),(7,3),(7,8),(7,9),(7,6),(7,1),(8,4),(8,5),(9,10),(9,9),(9,12);
/*!40000 ALTER TABLE `ItensPedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Pedido`
--

DROP TABLE IF EXISTS `Pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Pedido` (
  `idPedido` int(11) NOT NULL AUTO_INCREMENT,
  `idUsuario` int(11) NOT NULL,
  `dataPedido` datetime DEFAULT NULL,
  `quantidadeProduto` int(11) DEFAULT NULL,
  `preco` float(9,2) DEFAULT NULL,
  PRIMARY KEY (`idPedido`),
  KEY `idUsuario` (`idUsuario`),
  CONSTRAINT `Pedido_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Pedido`
--

LOCK TABLES `Pedido` WRITE;
/*!40000 ALTER TABLE `Pedido` DISABLE KEYS */;
INSERT INTO `Pedido` VALUES (1,4,'2020-06-27 21:44:59',3,182.30),(2,4,'2020-06-27 21:45:14',4,187.20),(3,4,'2020-06-27 21:45:31',1,43.50),(4,5,'2020-06-27 21:48:06',3,180.30),(5,5,'2020-06-27 21:48:31',4,204.60),(6,5,'2020-06-27 21:48:37',1,59.90),(7,6,'2020-06-27 21:50:58',6,370.70),(8,6,'2020-06-27 21:51:06',2,86.40),(9,6,'2020-06-27 21:51:14',3,150.70);
/*!40000 ALTER TABLE `Pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Produto`
--

DROP TABLE IF EXISTS `Produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Produto` (
  `idProduto` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(30) DEFAULT NULL,
  `saborPrincipal` varchar(30) DEFAULT NULL,
  `descricao` varchar(150) DEFAULT NULL,
  `preco` float(9,2) DEFAULT NULL,
  `imagem` varchar(50) DEFAULT NULL,
  `dataCadastro` datetime DEFAULT NULL,
  `idUsuario` int(11) DEFAULT NULL,
  PRIMARY KEY (`idProduto`),
  KEY `idUsuario` (`idUsuario`),
  CONSTRAINT `Produto_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Produto`
--

LOCK TABLES `Produto` WRITE;
/*!40000 ALTER TABLE `Produto` DISABLE KEYS */;
INSERT INTO `Produto` VALUES (1,'Chocolate com cobertura','chocolate','Bolo de chocolate com cobertura de dar água boca.',59.90,'Usuario_arquivo_103.jpg','2020-06-27 18:50:15',1),(2,'Chocolate com brigadeiro','chocolate','Bolo de chocolate com brigadeiro. O bolo preferido pelas crianças.',62.50,'Usuario_arquivo_230.jpg','2020-06-27 18:52:09',1),(3,'Chocolate cremoso','chocolate','Bolo de chocolate cremoso, ele derrete na boca.',57.90,'Usuario_arquivo_603.jpg','2020-06-27 18:53:57',1),(4,'Banana com canela','banana','Bolo de banana com canela. Simplesmente maravilhoso.',43.50,'Usuario_arquivo_616.jpg','2020-06-27 19:02:20',2),(5,'Banana frita','banana','Bolo de banana com cobertura de bananas fritas.',42.90,'Usuario_arquivo_281.jpg','2020-06-27 19:04:04',2),(6,'Banana com açúca queimada','banana','Bolo de banana com açúcar queimada na cobertura.',50.50,'Usuario_arquivo_575.jpg','2020-06-27 19:06:34',2),(7,'Laranja do campo','laranja','Bolo de laranja do campo.',32.50,'Usuario_arquivo_298.jpg','2020-06-27 19:09:43',3),(8,'Morango cremoso','morango','Bolo de morango cremoso. ',80.00,'Usuario_arquivo_240.jpg','2020-06-27 19:10:46',3),(9,'Laranja cremoso','laranja','Bolo de laranja cremosos. Muito bom.',59.90,'Usuario_arquivo_695.jpg','2020-06-27 19:11:53',3),(10,'Morango em pedaços','morango','Bolo de morango com pedaços de morango.',55.90,'Usuario_arquivo_286.jpg','2020-06-27 19:15:14',3),(11,'Laranja com mel','laranja','Bolo de laranja com mel como cobertura.',45.90,'Usuario_arquivo_526.jpg','2020-06-27 19:16:07',3),(12,'Morango estiloso','morango','Bolo de morango com excelente aspecto.',34.90,'Usuario_arquivo_816.jpg','2020-06-27 19:17:39',3);
/*!40000 ALTER TABLE `Produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Usuario`
--

DROP TABLE IF EXISTS `Usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Usuario` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(80) DEFAULT NULL,
  `nomeUsuario` varchar(80) DEFAULT NULL,
  `cpf` varchar(15) DEFAULT NULL,
  `email` varchar(80) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `cep` varchar(10) DEFAULT NULL,
  `senha` varchar(40) DEFAULT NULL,
  `tipoUsuario` tinyint(4) DEFAULT NULL,
  `dataCriacao` datetime DEFAULT NULL,
  `dataUltimoAcesso` datetime DEFAULT NULL,
  `imagem` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Usuario`
--

LOCK TABLES `Usuario` WRITE;
/*!40000 ALTER TABLE `Usuario` DISABLE KEYS */;
INSERT INTO `Usuario` VALUES (1,'rodrigo da matta soares','admin','000.000.000-00','rodrigo@dominio.com','00000-0000','00000-000','1234',0,'0001-01-01 00:00:01','2020-07-03 22:56:30','Usuario_arquivo_179.jpg'),(2,'Rodolfo Nascimento','rodolfo','166.851.234-52','rodolfo@mail.com','21 96762-5783','27530-145','1234',1,'2020-06-27 18:59:11','2020-06-27 19:00:59','Usuario_arquivo_322.png'),(3,'Ricardo Correa','ricardo','256.147.256-45','ricardo@hotmail.com','21 95468-2563','15236-478','1234',1,'2020-06-27 19:08:24','2020-06-27 21:52:32','Usuario_arquivo_41.jpg'),(4,'Lucas Gomes','lucas','165.856.321-98','lucas@yahoo.com','21 96766-3271','65935-802','1234',2,'2020-06-27 21:37:10','2020-06-28 18:53:28','Usuario_arquivo_24.jpeg'),(5,'Patricia Viena','patricia','256.658.852-63','Patricia@outlook.com','21 63254-1414','56236-652','1234',2,'2020-06-27 21:47:55','2020-07-02 18:05:07','Usuario_arquivo_324.jpeg'),(6,'Vitor Fábio','vitor','523.652.365-32','vitor@Hotmail.com','21 96523-4142','23564-523','1234',2,'2020-06-27 21:50:41','2020-06-27 21:50:41','Usuario_arquivo_42.jpg');
/*!40000 ALTER TABLE `Usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-04 15:12:58
