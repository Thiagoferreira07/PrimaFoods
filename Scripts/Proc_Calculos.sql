USE [dbRecruta]
GO
/****** Object:  StoredProcedure [dbo].[SP_CALCULO_AGIO_DESAGIO_ANIMAIS]    Script Date: 15/10/2020 14:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		THIAGO FERREIRA REZENDE
-- Create date: 17/10/2020
-- Description:	CALCULA VALOR BASE E OS RESPECTIVOS AGIOS E DESAGIOS
-- =============================================
--EXEC SP_CALCULO_AGIO_DESAGIO_ANIMAIS
CREATE PROCEDURE [dbo].[SP_CALCULO_AGIO_DESAGIO_ANIMAIS] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

delete from Evidencias
drop table if exists #ValoBase
drop table if exists #ValorTotal
drop table if exists #TabelaEvidencia

select *,
case 
		when Sexo = 'F' then peso * 50
		when Sexo = 'M' then peso * 100

end as ValorBase
into #ValoBase
from tabAnimais

	select *,
	case
		when qtdDentes = 0 then CAST(((ValorBase * 10/100) + ValorBase)as decimal (18,2))
		when qtdDentes >= 6  then  CAST((ValorBase * 90/100)as decimal (18,2))
		when qtdDentes = 2 or qtdDentes = 4  then  CAST((ValorBase)as decimal (18,2))

end as CalculoTotalDentes
into #ValorTotal
		from #ValoBase

		
	select *,
	case
		when qtdDentes = 0 then concat('ágio (acréscimo) de 10% = ' ,'R$ ',(CalculoTotalDentes - ValorBase))
		when qtdDentes >= 6  then concat('deságio (desconto) de 10% = ','R$ ',(ValorBase - CalculoTotalDentes))
		when qtdDentes = 2 or qtdDentes = 4  then 'não considerar ágio ou deságio'

end as Motivo
into #TabelaEvidencia
		from #ValorTotal

			


--select * from #ValoBase
--select * from #ValorTotal

insert into Evidencias
select Animal,ValorBase,CalculoTotalDentes,Motivo from #TabelaEvidencia



END
