using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class CertificateRepository : Repository<Certificate>
    {
        public IList<Certificate> GetList(CertificateStatus status,string specialtyId) {
            return BuildQuery(Context,status , specialtyId).ToList();
        }

        public IList<Certificate> GetList(CertificateStatus status,string specialtyId,int pageIndex,int pageSize,out int rowCount) {
            var query = BuildQuery(Context,status , specialtyId);
            rowCount = query.Count();
            int skip = GetSkip(pageIndex , pageSize , rowCount);
            return query.OrderByDescending(p => p.ReceiveDateTime)
                .ThenBy(p => p.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        IQueryable<Certificate> BuildQuery(Context db,CertificateStatus status,string specialtyId) {
            var query = db.Certificates.AsQueryable();
            if (!string.IsNullOrWhiteSpace(specialtyId)) {
                query = query.Where(p => p.SpecialtyId.Equals(specialtyId));
            }
            DateTime now = DateTime.Now;
            switch (status) {
                case CertificateStatus.All:
                    break;
                case CertificateStatus.Normal:
                    query = query.Where(p => p.ExpireDateTime >= now);
                    break;
                case CertificateStatus.UpComing:
                    DateTime t2 = now.AddDays(30);
                    query = query.Where(p => (p.ExpireDateTime <= t2 && p.ExpireDateTime >=now));
                    break;
                case CertificateStatus.Expired:
                    query = query.Where(p => p.ExpireDateTime < now);
                    break;
            }
            return query;
        }

        /// <summary>
        /// 根据证书编号或者用姓名搜索证书
        /// </summary>
        /// <param name="userNameOrNO"></param>
        /// <param name="specialty"></param>
        /// <returns></returns>
        public IList<Certificate> Serach(string userNameOrNO , string specialty = "") {
            var query = Context.Certificates.Where(p => p.EpmloyeeName.Contains(userNameOrNO) || p.Number == userNameOrNO);
            if (!string.IsNullOrWhiteSpace(specialty)) {
                query = query.Where(p => p.SpecialtyId.Equals(specialty));
            }
            return query.ToList();
        }
    }

    public enum CertificateStatus
    {
        All,
        /// <summary>
        /// 正常
        /// </summary>
        Normal ,
        /// <summary>
        /// 即将到期
        /// </summary>
        UpComing ,
        /// <summary>
        /// 已经过期
        /// </summary>
        Expired
    }
}
