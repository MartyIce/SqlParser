using Xunit;

namespace Maviicon.SqlParser.Tests
{
    public class SqlParserTest
    {
        [Fact]
        public void Simple()
        {
            string sql = @"SELECT ID, Name, Description FROM ACCOUNTS a JOIN Users u on u.AccountID = a.ID WHERE a.ID = 1";
            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
            Assert.Equal(3, parsedSql.Select.Fields.Count);
            Assert.Equal(2, parsedSql.Select.Tables.Count);
            Assert.Equal(1, parsedSql.Select.Joins.Count);
            Assert.Equal(1, parsedSql.Select.Wheres.Count);
        }
        [Fact]
        public void ThreeTables()
        {
            string sql = @"SELECT a.ID, b.Name, c.Description, c.Else FROM Table1 a JOIN Table2 b on a.Table2Id = b.Id JOIN Table3 c on c.Table3Id = b.id WHERE a.ID = 1 AND b.Something = 'Wow' OR c.Again = 11234";
            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
            Assert.Equal(4, parsedSql.Select.Fields.Count);
            Assert.Equal(3, parsedSql.Select.Tables.Count);
            Assert.Equal(2, parsedSql.Select.Joins.Count);
            Assert.Equal(3, parsedSql.Select.Wheres.Count);
        }
        [Fact]
        public void Case()
        {
            string sql = @"SELECT 
    (CASE 
        WHEN EXISTS(
            SELECT NULL AS [EMPTY]
            FROM [dbo].[UserAssociations] AS [t0]
            WHERE ([t0].[UserID] = 240541) AND ([t0].[AccountID] = 364891)
            ) THEN 1
        ELSE 0
     END) AS [value]";


            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
            Assert.Equal(1, parsedSql.Select.Fields.Count);
            Assert.Equal("[value]", parsedSql.Select.Fields[0].Alias);
            Assert.Equal(0, parsedSql.Select.Tables.Count);
            Assert.Equal(0, parsedSql.Select.Joins.Count);
            Assert.Equal(0, parsedSql.Select.Wheres.Count);
        }

        [Fact]
        public void SimpleFunction()
        {
            string sql = @"SELECT SUM(
                (CASE 
                    WHEN [t9].[IsUnviewed] = 1 THEN 14
                    ELSE 15
                 END)) AS [UnreadCount], COUNT(*) AS [TotalCount]";

            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
            Assert.Equal(2, parsedSql.Select.Fields.Count);
            Assert.Equal("[unreadcount]", parsedSql.Select.Fields[0].Alias);
            Assert.Equal("[totalcount]", parsedSql.Select.Fields[1].Alias);
            Assert.Equal(0, parsedSql.Select.Tables.Count);
            Assert.Equal(0, parsedSql.Select.Joins.Count);
            Assert.Equal(0, parsedSql.Select.Wheres.Count);
        }

        [Fact]
        public void GroupBy()
        {
            string sql = @"SELECT a.Type, Avg(a.Weight) from Accounts a Group By a.Type";
            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
            Assert.Equal(2, parsedSql.Select.Fields.Count);
            Assert.Equal(1, parsedSql.Select.Tables.Count);
            Assert.Equal(0, parsedSql.Select.Joins.Count);
            Assert.Equal(0, parsedSql.Select.Wheres.Count);
        }

        [Fact]
        public void BiggerCaseAndGroupBy()
        {
            string sql = @"SELECT SUM(
    (CASE 
        WHEN [t9].[IsUnviewed] = 1 THEN 14
        ELSE 15
     END)) AS [UnreadCount], COUNT(*) AS [TotalCount]
FROM (
    SELECT 1 AS [value], [t0].[MailboxFolderID], [t1].[PItemStatusEnumID], [t1].[CurrentOpTypeEnumID], [t1].[CurrentOpExtractStatusEnumID], [t1].[ID], [t0].[SystemFolderEnumID], [t1].[PurgeScansStatusEnumID], [t1].[IsScanOnly], [t0].[MailboxID], [t0].[IsUnviewed]
    FROM [dbo].[VItems] AS [t0]
    LEFT OUTER JOIN ([dbo].[PItems] AS [t1]
        INNER JOIN ([dbo].[PItemLocations] AS [t2]
            INNER JOIN ([dbo].[Locations] AS [t3]
                INNER JOIN [dbo].[Facilities] AS [t4] ON [t4].[ID] = [t3].[FacilityID]) ON [t3].[LocationID] = [t2].[LocationID]) ON [t2].[ID] = [t1].[PItemLocationID]
        LEFT OUTER JOIN [dbo].[Scans] AS [t5] ON [t5].[ID] = [t1].[PendingScanID]) ON [t1].[VItemID] = [t0].[ID]
    LEFT OUTER JOIN ([dbo].[VItemMailAttributes] AS [t6]
        LEFT OUTER JOIN [dbo].[VItemAddresses] AS [t7] ON [t7].[ID] = [t6].[SenderAddressID]) ON [t6].[VItemID] = [t0].[ID]
    INNER JOIN [dbo].[Mailboxes] AS [t8] ON [t8].[ID] = [t0].[MailboxID]
    ) AS [t9]
WHERE ([t9].[MailboxFolderID] IS NULL) AND ((([t9].[PItemStatusEnumID] = 1) AND (([t9].[CurrentOpTypeEnumID] IS NULL) OR (NOT (([t9].[CurrentOpTypeEnumID]) IN (24, 34, 64, 65, 32))) OR ([t9].[CurrentOpExtractStatusEnumID] = 5))) OR ((EXISTS(
    SELECT NULL AS [EMPTY]
    FROM [dbo].[Scans] AS [t10]
    WHERE ([t10].[ScanStatusEnumID] = 4) AND ([t10].[PItemID] = [t9].[ID])
    )) AND ([t9].[SystemFolderEnumID] = 0) AND ([t9].[PurgeScansStatusEnumID] = 10) AND ([t9].[IsScanOnly] = 1))) AND ([t9].[MailboxID] IN (11, 12, 13))
GROUP BY [t9].[value]";


            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
            Assert.Equal(1, parsedSql.Select.Fields.Count);
            Assert.Equal("[value]", parsedSql.Select.Fields[0].Alias);
            Assert.Equal(0, parsedSql.Select.Tables.Count);
            Assert.Equal(0, parsedSql.Select.Joins.Count);
            Assert.Equal(0, parsedSql.Select.Wheres.Count);
        }
        [Fact]
        public void Massive()
        {
            string sql = @"select q.RowNumber, q.InboxID, q.PItemID, q.VItemID, q.LicensePlate, q.WeightOz, q.EstimatedPageCount, q.HeightInch, q.DepthInch, q.LengthInch, q.PitemStatusEnumId, q.CurrentOpTypeEnumId,  q.CurrentOpExtractStatusEnumID, q.StorageFeeSuspended, q.StorageFeeDate, q.StorageFeeAmount, q.ReceivedDate, q.MailboxFolderID, q.SenderLabel, q.SenderAddress, q.FileStoreFileSetID, q.IsUnviewed, q.IsArchived, q.PieceSubType, q.FacilityStaticID, q.RecipientID, q.RecipientDisplayName, q.RecipientInboxID, q.ActualPageCount, q.ActualShipDate, q.ReceivedCarrierID, q.CarrierClassID, q.ScanID, q.PieceType, q.CheckCount, q.IsMICRSuspectCount, q.MailboxTypeEnumID, q.OperationPending, q.DepositRequestId, q.DepositRequestCompleted, q.ChecksFound, q.SequenceNumber, q.LocationID, q.LocationName, q.LocationCode, q.LocationType, q.ScanStatusEnumID, q.ScanStartDate, q.IsScanOnly from (SELECT i.ID AS InboxID, p.ID AS PItemID, v.ID AS VItemID, p.LicensePlate, p.WeightOz, p.EstimatedPageCount, p.HeightInch, p.DepthInch, p.LengthInch, p.PitemStatusEnumId, p.CurrentOpTypeEnumId, p.CurrentOpExtractStatusEnumID, p.StorageFeeSuspended AS StorageFeeSuspended, p.StorageFeeDate AS StorageFeeDate, p.StorageFeeAmount AS StorageFeeAmount, v.ReceivedDate, v.MailboxFolderID, v.SenderLabel, v.SenderAddress, v.FileStoreFileSetID, v.IsUnviewed, ISNULL(v.IsArchived, 0) AS IsArchived, LOWER(vte.Name) AS PieceSubType, f.StaticID AS FacilityStaticID, r.ID AS RecipientID, r.DisplayName AS RecipientDisplayName, r.InboxID AS RecipientInboxID, s.ActualPageCount, ship.ActualShipDate, vima.ReceivedCarrierID, vima.CarrierClassID, s.ID AS ScanID, 'mail' AS PieceType, ISNULL(depositChecks.CheckCount, 0) AS CheckCount, ISNULL(depositChecks.IsMICRSuspectCount, 0) AS IsMICRSuspectCount, m.MailboxTypeEnumID, CASE WHEN p.PItemStatusEnumID = 1 AND p.CurrentOpTypeEnumId IS NOT NULL AND p.CurrentOpExtractStatusEnumID IN (1, 2, 3) THEN 1 ELSE 0 END as OperationPending, pdr.ID AS DepositRequestId, pdr.CompletedDate AS DepositRequestCompleted, ISNULL(pdr.ChecksFound, 0) AS ChecksFound, pl.SequenceNumber, l.LocationID, l.Name AS LocationName, l.LocationCode, lte.Name AS LocationType, s.ScanStatusEnumID, s.StartDate AS ScanStartDate, p.IsScanOnly , ROW_NUMBER() OVER(ORDER BY v.ReceivedDate DESC) as 'RowNumber' FROM mailboxes m with (nolock) left join inboxes i with (nolock) on m.AccountID = i.AccountID join VItems v with (nolock) on v.MailboxID = m.ID LEFT JOIN VItemTypeEnum vte WITH (nolock) ON v.VItemTypeEnumID = vte.ID join PItems p with (nolock) on p.VItemID = v.ID join PItemLocations pl with (nolock) on pl.ID = p.PItemLocationID join Locations l with (nolock) on l.LocationID = pl.LocationID join LocationTypeEnum lte with (nolock) on l.LocationTypeEnumID = lte.ID join Facilities f with (nolock) on f.ID = l.FacilityID left join MailboxFolders mf with (nolock) on v.MailboxFolderID = mf.ID left join Recipients r with (nolock) on m.PrimaryRecipientID = r.ID and r.InboxID = i.ID OUTER APPLY (select top 1 * from Scans s2 with (nolock) where s2.PItemID = p.ID and s2.ScanStatusEnumID not in (5, 6, 7) order by s2.EndDate desc) s left join Shipments ship with (Nolock) on p.ShipmentID = ship.ID and ship.ActualShipDate IS NOT NULL join VItemMailAttributes vima with (Nolock) on vima.VItemID = v.ID left join (select VItemID, count(*) as 'CheckCount', sum(CAST(IsMICRSuspect AS INT)) as 'IsMICRSuspectCount' from DepositChecks depositChecks with(nolock) group by VItemID) depositChecks on depositChecks.VItemID = v.ID left join PItemDepositRequests pdr with (nolock) on pdr.PItemID = p.ID where i.ID = 186500  and ((p.PitemStatusEnumId in (1,9) and (p.CurrentOpTypeEnumID IS NULL OR p.CurrentOpTypeEnumID NOT IN (24,34,64,65,32) OR p.CurrentOpExtractStatusEnumID IN (5))) OR (p.PItemStatusEnumID IN (3,4) AND v.SystemFolderEnumID = 0 AND p.IsScanOnly = 1 AND p.PurgeScansStatusEnumID = 0 AND EXISTS (SELECT * FROM scans trashScans WITH (NOLOCK) where trashScans.PItemID = p.id AND trashScans.ScanStatusEnumID = 4) ) ) and (m.PrimaryRecipientID IS NULL OR r.InboxID = 186500 ) and ISNULL(v.IsArchived, 0) = 0 and v.MailboxFolderID is null and m.ID in (346622,346623,346624,346625,346626,346627,346628,361001,1865000,1865001,1865002,1865003,1865004,1865005,1865006,1865007,1865008,1865009,3466220,3466221 ) ) q where q.RowNumber >= 1 and q.RowNumber < 26 ORDER BY q.ReceivedDate DESC, q.PItemID DESC";


            SqlParser sp = new SqlParser();
            var parsedSql = sp.Parse(sql);
        }
    }

}
