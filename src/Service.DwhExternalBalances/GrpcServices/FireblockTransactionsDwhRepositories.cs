﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.GrpcServices
{
    public class FireblockTransactionsDwhRepositories
    {
        private readonly DbContextOptionsBuilder<DwhContext> _dwhContextOptionsBuilder;
        private readonly ILogger<FireblockTransactionsDwhRepositories> _logger;

        public FireblockTransactionsDwhRepositories(DbContextOptionsBuilder<DwhContext> dwhContextOptionsBuilder,
            ILogger<FireblockTransactionsDwhRepositories> logger)
        {
            _dwhContextOptionsBuilder = dwhContextOptionsBuilder;
            _logger = logger;
        }

        public async Task<List<FireblockTransaction>> GetTransactionWithinFireblockAsync(DateTime from, DateTime to)
        {
            try
            {
                await using var ctx = new DwhContext(_dwhContextOptionsBuilder.Options);
                var query =
                    $@"SELECT [TxHash]
                          ,[FireblocksAssetId]
                          ,[Id]
                          ,[CreatedDate]
                          ,[UpdatedDate]
                          ,[FireblocksFeeAssetId]
                          ,[Amount]
                          ,[Fee]
                          ,[Status]
                          ,[SourceAddress]
                          ,[DestinationAddress]
                          ,[Source_Id] as SourceId
                          ,[Source_Type] as SourceType
                          ,[Source_Name] as SourceName
                          ,[Destination_Id] as DestinationId
                          ,[Destination_Type] as DestinationType
                          ,[Destination_Name] as DestinationName
                          ,[AssetSymbol]
                          ,[AssetNetwork]
                          ,[FeeAssetSymbol]
                          ,[FeeAssetNetwork]
                          ,[AssetIndexPrice]
                          ,[FeeAssetIndexPrice]
                    	  ,[Fee] * [FeeAssetIndexPrice] as FeeUsd
                    FROM [report].[TransactionWithinFireblocks]
                    WHERE [UpdatedDate] >= @From and [UpdatedDate] < @To ";

                var response = await ctx.Database.GetDbConnection()
                    .QueryAsync<FireblockTransaction>(query, new
                    {
                        From = from,
                        To = to
                    });

                var transaction = response == null ? new List<FireblockTransaction>() : response.ToList();

                return transaction;
            }
            catch (Exception e)
            {
                _logger.LogError("GetTransactionWithinFireblockAsync can't get data: {ex}",e.Message);
                throw;
            }
        }
        
        public async Task<List<FireblockTransaction>> GetTransactionOutsideFireblockAsync(DateTime from, DateTime to)
        {
            try
            {
                await using var ctx = new DwhContext(_dwhContextOptionsBuilder.Options);
                var query =
                    $@"SELECT [TxHash]
                            ,[FireblocksAssetId]
                            ,[Id]
                            ,[CreatedDate]
                            ,[UpdatedDate]
                            ,[FireblocksFeeAssetId]
                            ,[Amount]
                            ,[Fee]
                            ,[Status]
                            ,[SourceAddress]
                            ,[DestinationAddress]
                            ,[Source_Id] as SourceId
                            ,[Source_Type] as SourceType
                            ,[Source_Name] as SourceName
                            ,[Destination_Id] as DestinationId
                            ,[Destination_Type] as DestinationType
                            ,[Destination_Name] as DestinationName
                            ,[AssetSymbol]
                            ,[AssetNetwork]
                            ,[FeeAssetSymbol]
                            ,[FeeAssetNetwork]
                            ,[AssetIndexPrice]
                            ,[FeeAssetIndexPrice]
                        	,[Fee] * [FeeAssetIndexPrice] as FeeUsd
                        FROM [report].[TrunsactionOutsideFireblocks]
                        WHERE [UpdatedDate] >= @From AND [UpdatedDate] < @To";

                var response = await ctx.Database.GetDbConnection()
                    .QueryAsync<FireblockTransaction>(query, new
                    {
                        From = from,
                        To = to
                    });

                var transaction = response == null ? new List<FireblockTransaction>() : response.ToList();

                return transaction;
            }
            catch (Exception e)
            {
                _logger.LogError("GetTransactionOutsideFireblockAsync can't get data: {ex}",e.Message);
                throw;
            }
        }
        
        public async Task<List<FireblockTransaction>> GetTransactionToFireblock(DateTime from, DateTime to)
        {
            try
            {
                await using var ctx = new DwhContext(_dwhContextOptionsBuilder.Options);
                var query =
                    $@"SELECT [TxHash]
                            ,[FireblocksAssetId]
                            ,[Id]
                            ,[CreatedDate]
                            ,[UpdatedDate]
                            ,[FireblocksFeeAssetId]
                            ,[Amount]
                            ,[Fee]
                            ,[Status]
                            ,[SourceAddress]
                            ,[DestinationAddress]
                            ,[Source_Id] as SourceId
                            ,[Source_Type] as SourceType
                            ,[Source_Name] as SourceName
                            ,[Destination_Id] as DestinationId
                            ,[Destination_Type] as DestinationType
                            ,[Destination_Name] as DestinationName
                            ,[AssetSymbol]
                            ,[AssetNetwork]
                            ,[FeeAssetSymbol]
                            ,[FeeAssetNetwork]
                            ,[AssetIndexPrice]
                            ,[FeeAssetIndexPrice]
                        	,[Fee] * [FeeAssetIndexPrice] as FeeUsd
                        FROM [report].[WithdrawalsFromExchanges]
                        WHERE [UpdatedDate] >= @From AND [UpdatedDate] < @To";

                var response = await ctx.Database.GetDbConnection()
                    .QueryAsync<FireblockTransaction>(query, new
                    {
                        From = from,
                        To = to
                    });

                var transaction = response == null ? new List<FireblockTransaction>() : response.ToList();

                return transaction;
            }
            catch (Exception e)
            {
                _logger.LogError("GetTransactionToFireblock can't get data: {ex}",e.Message);
                throw;
            }
        }
        
    }
}