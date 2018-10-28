using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Neo.SmartContract
{
    public class ICO_Template : Framework.SmartContract
    {
        //Tender Settings
        public static string Name() => "name of the tender";
        public static readonly byte[] Owner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();
        private static readonly byte[] neo_asset_id = { 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
        private const int tender_start_date = 1506787200;
        private const int totalNFTSupply = 1; //one token, one tender
        private const int NFTdivisibility = 100; //maximum number of competing companies in a tender
        private const int tender_end_date = 1538323200; // 
        private static readonly byte[] tender_NFT_addr = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();

        [DisplayName("transfer")]
        public static event Action<byte[], byte[], BigInteger> Transferred;

        public static Object Main(string operation, params object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                if (Owner.Length == 20)
                {
                    // if param Owner is script hash
                    return Runtime.CheckWitness(Owner);
                }
                else if (Owner.Length == 33)
                {
                    // if param Owner is public key
                    byte[] signature = operation.AsByteArray();
                    return VerifySignature(signature, Owner);
                }
            }
            else if (Runtime.Trigger == TriggerType.Application)
            {
                if (operation == "deploy") return Deploy();
                if (operation == "name") return Name();
                if (operation == "hasEnded") 
                {
                    // if tender_end_date < currentBlockHeight
                    // Issue: passage of time. An on-chain service would need to
                    // be used to provide blockheight estimation for a certain 
                    // expected point in time. General Blockchain issue.
                }
                if (operation == "balanceOf")
                {
                    if (args.Length != 1) return 0;
                    byte[] account = (byte[])args[0];
                    return BalanceOf(account);
                }
            }
            return false;
        }

        // initialization parameters, only once
        public static bool Deploy()
        {
            Storage.Put(Storage.CurrentContext, "tenderStart", tender_start_date);
            Storage.Put(Storage.CurrentContext, "tenderEnd", tender_end_date);
            Storage.Put(Storage.CurrentContext, "tenderName", tender_end_date);
            Storage.Put(Storage.CurrentContext, "tenderDesc", tender_end_date);
            Storage.Put(Storage.CurrentContext, "tenderNFTaddr", tender_end_date);
            return true;
        }

        // function that is always called when someone wants to transfer tokens.
        public static bool InvokeNFTContract()
        {
            // check balance of current Tendercontract owner if sufficient for 
            // NFT contract deployment cost
            
            // get tenderToken setting variables
            
            // deploy tenderToken contract
            
            return true;
        }

        public static bool CloseTender()
        {
            // call TenderTokenNFT contract
            
            // get list of token holder using ownersOf(TenderToken)
            
            // compute highest value token holding address iterating over
            // ownersOf() and looking for higest owner 
            // EXPENSIVE IF UNSORTED OUTPUT OF ownersOf()
            // if max(balanceOf(ownersOf(TenderToken))) == totalNFTSupply
            // check if blockheight is below enddate
                // {
                    // closeTender();
                // }
            
            
            
            return true;
        }
        
        // get smart contract script hash
        private static byte[] GetReceiver()
        {
            return ExecutionEngine.ExecutingScriptHash;
        }

    }
}
